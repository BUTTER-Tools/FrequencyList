using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Windows.Forms;
using System.Drawing;
using PluginContracts;
using OutputHelperLib;
using System.IO;


namespace FrequencyList
{
    public class FrequencyList : Plugin
    {


        public string[] InputType { get; } = { "Tokens" };
        public string OutputType { get; } = "OutputArray";

        public Dictionary<int, string> OutputHeaderData { get; set; } = new Dictionary<int, string>() { { 0, "TokenizedText" } };
        public bool InheritHeader { get; } = false;

        #region Plugin Details and Info

        public string PluginName { get; } = "Frequency List";
        public string PluginType { get; } = "Corpus Tools";
        public string PluginVersion { get; } = "1.0.11";
        public string PluginAuthor { get; } = "Ryan L. Boyd (ryan@ryanboyd.io)";
        public string PluginDescription { get; } = "Extracts and outputs a frequency list of n-grams from your texts. Includes information such as raw frequency of each n-gram, the number and percent of documents that each n-gram appears in, and the Inverse Document Frequency weight for each n-gram. " + 
                                                   "2-grams and higher can also be filtered out using a minimum threshold along some collocation metric, such as logDice or Normalized Pointwise Mutual Information (NPMI). The NPMI formula used by this plugin is described in " + Environment.NewLine + Environment.NewLine +
                                                   "Bouma, G. (2009). Normalized (Pointwise) Mutual Information in Collocation Extraction. Proceedings of the Biennial GSCL Conference, 31–40. Tübingen: Gunter Narr Verlag." + Environment.NewLine + Environment.NewLine +
                                                   "The frequency list is pooled while the pipeline is being run, and then all outputs are calculated at the very end. Note that this means that the plugin can be extremely memory-intensive, " +
                                                   "particularly if you are collecting higher-number n-grams (e.g., 3-grams, 4-grams, etc.). As such, you may want (or need) to use a lower \"pruning\" value (which will make the plugin prune your frequency list at " +
                                                   "more regular intervals) if you are building a frequency list that looks for higher-number n-grams.";
        public string PluginTutorial { get; } = "https://youtu.be/utgoXVB-2wM";
		public bool TopLevel { get; } = false;

        public Icon GetPluginIcon
        {
            get
            {
                return Properties.Resources.icon;
            }
        }

        #endregion



        private uint nGramN { get; set; } = 1;
        private string Quotes = "\"";
        private string Delimiter = ",";
        private decimal minFreq { get; set; } = 5;
        private decimal pruneInterval { get; set; } = 10000000;
        private decimal minTokenCount { get; set; } = 10;
        private decimal minObsPct { get; set; } = (decimal)0.10;
        private bool retainSubgrams { get; set; } = true;
        private double collocateFilterParameter { get; set; } = 0.50;
        private string collocateFilterMetric { get; set; } = "NPMI";
        private bool OutputUsesUnderscores { get; set; } = false;

        private static decimal[] ignored;

        //private static long TotalNumberOfDocs;
        //private static long ngramPruneTracker;

        private ConcurrentDictionary<string, ulong> TotalNumberOfDocs;
        private ConcurrentDictionary<string, ulong> ngramPruneTracker;

        private ConcurrentDictionary<string, decimal[]> wordFreqs { get; set; }
        private Dictionary<uint, decimal> nGramCounts = new Dictionary<uint, decimal>();


        public void ChangeSettings()
        {

            using (var form = new SettingsForm_FrequencyList(nGramN, minFreq, minObsPct, pruneInterval, minTokenCount,
                (decimal)collocateFilterParameter, collocateFilterMetric, retainSubgrams, OutputUsesUnderscores))
            {


                form.Icon = Properties.Resources.icon;
                form.Text = PluginName;


                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    nGramN = form.nGramValue;
                    minFreq = form.minimumFreq;
                    minObsPct = form.minObsPct;
                    pruneInterval = form.pruneInterval;
                    minTokenCount = form.minTokenCount;
                    collocateFilterParameter = (double)form.npmiFilterMin;
                    collocateFilterMetric = form.collocateFilterMetric;
                    OutputUsesUnderscores = form.OutputUsesUnderscores;
                    //retainSubgrams = form.retainSubgrams;
                }
            }

        }





        public Payload RunPlugin(Payload Input)
        {


            Payload pData = new Payload();
            pData.FileID = Input.FileID;
            pData.SegmentID = Input.SegmentID;

            for (int i = 0; i < Input.StringArrayList.Count; i++)
            {

                //if there aren't enough words in the text, we ignore it and move on
                if (Input.StringArrayList[i].Length < minTokenCount) continue;

                //if we have enough tokens, we increment the document count in a threadsafe manner
                TotalNumberOfDocs.AddOrUpdate("Docs", 1, (id, count) => count + 1);

                //build an array of all of the ngrams that we're keeping
                string[] ngramArray = BuildNgramArray(Input.StringArrayList[i], 1, nGramN);

                HashSet<string> foundWords = new HashSet<string>();

                for (int j = 0; j < ngramArray.Length; j++)
                {

                    decimal docValue = 0;
                    if (!foundWords.Contains(ngramArray[j]))
                    {
                        foundWords.Add(ngramArray[j]);
                        docValue = 1;
                    }

                    //this is really just a 2-element array
                    //element 0 = raw frequency
                    //element 1 = number of documents
                    wordFreqs.AddOrUpdate(ngramArray[j], new decimal[] { 1, 1 }, (k, v) => new decimal[] { v[0] + 1, v[1] + docValue });
                   
                }

                //add the number of n-grams that we just processed to the pruning tracker, in a threadsafe manner
                ngramPruneTracker.AddOrUpdate("Count", (ulong)ngramArray.Length, (id, count) => count + (ulong)ngramArray.Length);
                //IncrementPruneTracker(ngramArray.Length);

                //if it's time to prune...
                if (ngramPruneTracker["Count"] >= pruneInterval)

                {
                    //set the tracker back go zero
                    ngramPruneTracker.AddOrUpdate("Count", 0, (id, count) => 0);

                    string[] wordKeys = wordFreqs.Keys.ToArray();

                    //iterate over all of the keys that we have and drop those with a frequency
                    //less than our minimum threshold
                    for(int dropCount = 0; dropCount < wordKeys.Length; dropCount++)
                    {

                        decimal[] temp_val;
                        wordFreqs.TryGetValue(wordKeys[dropCount], out temp_val);
                        if (temp_val != null && temp_val[0] < minFreq)
                        {
                                
                            wordFreqs.TryRemove(wordKeys[dropCount], out ignored);
                        }

                    }

                }

            }

            return new Payload();

        }





        public void Initialize()
        {
            //TotalNumberOfDocs = 0;

            TotalNumberOfDocs = new ConcurrentDictionary<string, ulong>();
            TotalNumberOfDocs.TryAdd("Docs", 0);
            ngramPruneTracker = new ConcurrentDictionary<string, ulong>();
            ngramPruneTracker.TryAdd("Count", 0);

            wordFreqs = new ConcurrentDictionary<string, decimal[]>();
            nGramCounts = new Dictionary<uint, decimal>();
            OutputHeaderData = new Dictionary<int, string>() { 
                                                                { 0, "Frequency" },
                                                                { 1, "Documents" },
                                                                { 2, "ObsPct" },
                                                                { 3, "IDF" },
                                                                { 4, "PhraseLength" },
                                                                { 5, "NPMI" },
                                                                { 6, "logDice" },
                                                                };
        }



        



        public bool InspectSettings()
        {
            return true;            
        }








        public Payload FinishUp(Payload Input)
        {

            //the last thing that we're going to do is build up an entire output set, which will (in theory)
            //be passed on to a CSV Output Writer plugin
            
            Payload OutputData = new Payload();
            OutputData.FileID = "";

            //let's go ahead and sort out the n-grams that we're going to write here
            string[] nGramKeys = wordFreqs.Keys.ToArray();

            //trim anything that falls below minimum frequency
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //note that we're also forcing n-grams out that are problematic.
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //sometimes the ways in which things get tokenized
            //are still *technically* correct, but make things difficult downstream. for example,
            //the phrase ". . . . . ." is a 3-gram -- [[., .], [., .], [., .]] -- but this is a
            //nightmare to parse out for other purposes. the entire structure of this plugin could
            //be redone so that it retains specific token information but, honestly, these are 
            //fringe cases that would drive up the memory costs if we wanted to preserve all of that
            //data. maybe down the road this will be redone but, for now, we just drop these really
            //difficult fringe cases and move on
            for (int i = 0; i < nGramKeys.Length; i++)
            {

                decimal ndocs = wordFreqs[nGramKeys[i]][1];
                decimal obsPct = (ndocs / TotalNumberOfDocs["Docs"]) * 100;

                if (wordFreqs[nGramKeys[i]][0] < minFreq || obsPct < minObsPct || nGramKeys[i].Split().Length > nGramN) wordFreqs.TryRemove(nGramKeys[i], out ignored);
            }
            //get the keys again now that we've removed low freq n-grams
            nGramKeys = wordFreqs.Keys.ToArray();nGramKeys = wordFreqs.Keys.ToArray();


            //now that we know what n-grams we're retaining, we can calculate pointwise mutual information where we want to
            //what that means, however, is that we need to know how many of each n-gram we have
            //we'll create the dictionary here, but fill it out later
            
            for (int i = 1; i <= nGramN; i++) nGramCounts.Add((uint)i, 0);


            //we can actually sort our list of n-grams so that we can write them in descending order
            //to do so, we create a new array that we'll use to sort that contains the frequencies for each n-gram
            decimal[] nGramFreqs = new decimal[nGramKeys.Length];

            for (int i = 0; i < nGramKeys.Length; i++)
            {
                //get the actual frequencies of each n-gram
                nGramFreqs[i] = wordFreqs[nGramKeys[i]][0];
                //and, since we're already looping over the array, we'll simultaneously get the
                //number of times we have each type of n-gram (e.g., 1-gram counts, 2-gram counts, etc etc)
                nGramCounts[(uint)nGramKeys[i].Split().Length] += wordFreqs[nGramKeys[i]][0];
            }



            

            //Sort ascending by frequency
            Array.Sort(nGramFreqs, nGramKeys);
            nGramFreqs = new decimal[] { };
            //reverse so that it's now going to be descending
            Array.Reverse(nGramKeys);






            //now we go in and actually generate the rows of output that need to be written
            for (int i = 0; i < nGramKeys.Length; i++)
            {

                //first, we calculate PMI (pointwise mutual information)
                //we're going to do this first because we'll just go ahead
                //and move on to the next n-gram if we end up losing this one
                //due to the PMI filter

                int PhraseLength = nGramKeys[i].Split().Length;
                double normalized_pmi = 0;
                double logDice = 0;
                double comparisonMetric = double.MinValue;

                if (PhraseLength > 1) { 
                    try
                    {
                        normalized_pmi = CalculateNPMI(nGramKeys[i], PhraseLength);
                        logDice = CalculateLogDice(nGramKeys[i], PhraseLength);

                        switch (collocateFilterMetric)
                        {
                            case "NPMI":
                                comparisonMetric = normalized_pmi;
                                break;
                            case "logDice":
                                comparisonMetric = logDice;
                                break;
                        }

                    }
                    catch
                    {
                        continue;
                    }

                    if (comparisonMetric < collocateFilterParameter)
                    {
                        continue;
                    }




                }


                OutputData.SegmentNumber.Add((ulong)(i + 1)); //n-gram rank


                string nGramToWrite = nGramKeys[i];
                if (OutputUsesUnderscores) nGramToWrite = nGramToWrite.Replace(' ', '_');

                OutputData.SegmentID.Add(nGramToWrite); //actual n-gram

                

                string[] nGramOutputArray = new string[7];
                nGramOutputArray[0] = wordFreqs[nGramKeys[i]][0].ToString(); // raw frequency
                nGramOutputArray[1] = wordFreqs[nGramKeys[i]][1].ToString(); // number of documents
                nGramOutputArray[2] = ((wordFreqs[nGramKeys[i]][1] / TotalNumberOfDocs["Docs"]) * 100).ToString(); // observation percent
                nGramOutputArray[3] = Math.Log((double)(TotalNumberOfDocs["Docs"] / wordFreqs[nGramKeys[i]][1])).ToString(); // IDF

                
                nGramOutputArray[4] = PhraseLength.ToString(); // PhraseLength
                nGramOutputArray[5] = ""; //Pointwise Mutual Information
                nGramOutputArray[6] = ""; //logDice

                if (PhraseLength > 1)
                {
                    nGramOutputArray[5] = normalized_pmi.ToString();
                    nGramOutputArray[6] = logDice.ToString();
                }



                OutputData.StringArrayList.Add(nGramOutputArray);

            }


            //since we need to keep all of the n-gram data to calculate pointwise mutual information, we don't
            //clear out the concurrentdictionary until we're pretty much done. might do something to change
            //this later to make it more memory efficient
            wordFreqs.Clear();


            return (OutputData);
        }



        



        private string[] BuildNgramArray(string[] TokenizedText_For_Ngrams, int Ngram_N_Min, uint Ngram_N_Max)
        {

            List<string> ngrams = new List<string>();

            for (long i = 0; i < TokenizedText_For_Ngrams.Length; i++)
            {
                //builds our ngrams...
                for (uint j = Ngram_N_Max; j > Ngram_N_Min - 1; j--)
                {
                    if (i + j <= TokenizedText_For_Ngrams.Length)
                    {
                        string[] token_builder = new string[j];
                        Array.Copy(TokenizedText_For_Ngrams, i, token_builder, 0, j);

                        ngrams.Add(string.Join(" ", token_builder));

                    }

                }

            }

            
            
            return (ngrams.ToArray());


        }









        //a nice, thread-safe way to track the number of documents that we're including
        //private static void IncrementDocCount()
        //{
        //    Interlocked.Increment(ref TotalNumberOfDocs);
        //}

        //private static void IncrementPruneTracker(int ngramCount)
        //{
        //    Interlocked.Add(ref ngramPruneTracker, ngramCount);
        //}


        private double CalculateNPMI(string phrase, int phraseLengthAsInt)
        {

            uint phraseLengthAsUInt = (uint)phraseLengthAsInt;

            //the numerator of the equation is the number of times this specific n-gram appears
            //divided by the total number of this type of n-gram in the dataset (e.g., total number of 2-grams).
            double pmi_numerator = (double)(wordFreqs[phrase][0] / nGramCounts[phraseLengthAsUInt]);

            string[] subgrams = phrase.Split();
            //now we have to get the probabilities of the two components of the n-gram, a la:
            //http://mlwiki.org/index.php/Collocation_Extraction#.24n.24-Gram_Collocations

            //                                      (   P(w1, ... , wn + 1)        )
            //  PMI((w1, ..., wn),wn + 1) = log2   (   -----------------------      )
            //                                      ( P(w1, ... , wn) * P(wn + 1)  )

            //take all but the last word in the n-gram
            string subgramLeft = String.Join(" ", subgrams.Take(phraseLengthAsInt - 1));
            //take the last word in the n-gram
            string subgramRight = subgrams[phraseLengthAsInt - 1];

            //get the probability of the left part of the n-gram
            double pmi_denominator = (double)(wordFreqs[subgramLeft][0] / nGramCounts[phraseLengthAsUInt - 1]);
            //and multiply by the last word in the n-gram
            pmi_denominator = pmi_denominator * (double)(wordFreqs[subgramRight][0] / nGramCounts[1]);
            

            //double pmi = Math.Log(pmi_numerator / pmi_denominator, 2);

            //https://pdfs.semanticscholar.org/1521/8d9c029cbb903ae7c729b2c644c24994c201.pdf
            //Normalized (Pointwise) Mutual Information in Collocation Extraction
            //Gerlof Bouma
            double normalized_pmi = Math.Log(pmi_numerator / pmi_denominator) / (Math.Log(pmi_numerator) * -1);

            return normalized_pmi;
        }

        private double CalculateLogDice(string phrase, int phraseLengthAsInt)
        {

            uint phraseLengthAsUInt = (uint)phraseLengthAsInt;

            
            double logDice_numerator = 2 * (double)(wordFreqs[phrase][0]);

            string[] subgrams = phrase.Split();
            //now we have to get the frequencies of the two components of the n-gram, a la:
            //http://mlwiki.org/index.php/Collocation_Extraction#.24n.24-Gram_Collocations

            //                                              (   2 * P(w1, ... , wn + 1)     )
            //  LogDice((w1, ..., wn),wn + 1) = 14 + log2   (   -----------------------     )
            //                                              ( P(w1, ... , wn) + P(wn + 1)  )

            //take all but the last word in the n-gram
            string subgramLeft = String.Join(" ", subgrams.Take(phraseLengthAsInt - 1));
            //take the last word in the n-gram
            string subgramRight = subgrams[phraseLengthAsInt - 1];

            //get the frequency of the left part of the n-gram
            //and add the frequency of the last word in the n-gram
            double logDice_denominator = (double)(wordFreqs[subgramLeft][0] + wordFreqs[subgramRight][0]);

            //https://pdfs.semanticscholar.org/1521/8d9c029cbb903ae7c729b2c644c24994c201.pdf
            //Normalized (Pointwise) Mutual Information in Collocation Extraction
            //Gerlof Bouma
            double logDice = 14 + Math.Log(logDice_numerator / logDice_denominator, 2);

            return logDice;
        }










        #region Import/Export Settings
        public void ImportSettings(Dictionary<string, string> SettingsDict)
        {
            nGramN = uint.Parse(SettingsDict["nGramN"]);
            minFreq = Decimal.Parse(SettingsDict["minFreq"]);
            minObsPct = Decimal.Parse(SettingsDict["minObsPct"]);
            pruneInterval = Decimal.Parse(SettingsDict["pruneInterval"]);
            minTokenCount = Decimal.Parse(SettingsDict["minTokenCount"]);
            collocateFilterParameter = Double.Parse(SettingsDict["collocateFilterParameter"]);
            collocateFilterMetric = SettingsDict["collocateFilterMetric"];
            OutputUsesUnderscores = Boolean.Parse(SettingsDict["OutputUsesUnderscores"]);
        }

        public Dictionary<string, string> ExportSettings(bool suppressWarnings)
        {
            Dictionary<string, string> SettingsDict = new Dictionary<string, string>();

            SettingsDict.Add("nGramN", nGramN.ToString());
            SettingsDict.Add("minFreq", minFreq.ToString());
            SettingsDict.Add("minObsPct", minObsPct.ToString());
            SettingsDict.Add("pruneInterval", pruneInterval.ToString());
            SettingsDict.Add("minTokenCount", minTokenCount.ToString());
            SettingsDict.Add("collocateFilterParameter", collocateFilterParameter.ToString());
            SettingsDict.Add("collocateFilterMetric", collocateFilterMetric);
            SettingsDict.Add("OutputUsesUnderscores", OutputUsesUnderscores.ToString());
            return (SettingsDict);
        }
        #endregion









    }
}
