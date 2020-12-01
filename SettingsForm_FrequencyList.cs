using System.IO;
using System.Text;
using System.Windows.Forms;
using System;

namespace FrequencyList
{
    internal partial class SettingsForm_FrequencyList : Form
    {


        #region Get and Set Options

      
        public uint nGramValue { get; set; }
        public decimal minimumFreq { get; set; }
        public decimal minObsPct { get; set; }
        public decimal pruneInterval { get; set; }
        public decimal minTokenCount { get; set; }
        public decimal npmiFilterMin { get; set; }
        public bool retainSubgrams { get; set; }
        public string collocateFilterMetric { get; set; }
        public bool OutputUsesUnderscores { get; set; }
       #endregion



        public SettingsForm_FrequencyList(uint NgramN, decimal minFreq, decimal minObsPctInput, decimal pruneInt, decimal minTok,
            decimal collocateFilterParameterInput, string collocateFilterMetricInput, bool retainSubgramsInput, bool UseUnderscoresIncoming)
        {
            InitializeComponent();


            CollocateFilterMetricDropdown.Items.AddRange(new string[] {
                                                                    "NPMI",
                                                                    "logDice"
                                                                    });
            try
            {
                CollocateFilterMetricDropdown.SelectedIndex = CollocateFilterMetricDropdown.FindStringExact(collocateFilterMetricInput);
            }
            catch
            {
                CollocateFilterMetricDropdown.SelectedIndex = 0;
            }



            NgramBox.Value = (decimal)NgramN;
            minFreqUpDown.Value = minFreq;
            MinObsPctNumericUpDown.Value = minObsPctInput;
            PruneIntervalUpDown.Value = pruneInt;
            minTokenCountBox.Value = minTok;
            CollocateFilterParameterNumericUpDown.Value = collocateFilterParameterInput;
            //RetainSubgramsCheckbox.Checked = retainSubgramsInput;
            retainSubgrams = retainSubgramsInput;
            ReplaceSpaceCheckbox.Checked = UseUnderscoresIncoming;
                       


        }





        private void OKButton_Click(object sender, System.EventArgs e)
        {

            this.nGramValue = Convert.ToUInt32(NgramBox.Value);
            this.minimumFreq = minFreqUpDown.Value;
            this.minObsPct = MinObsPctNumericUpDown.Value;
            this.pruneInterval = PruneIntervalUpDown.Value;
            this.minTokenCount = minTokenCountBox.Value;
            this.npmiFilterMin = CollocateFilterParameterNumericUpDown.Value;
            this.collocateFilterMetric = CollocateFilterMetricDropdown.SelectedItem.ToString();
            //this.retainSubgrams = RetainSubgramsCheckbox.Checked;
            this.OutputUsesUnderscores = ReplaceSpaceCheckbox.Checked;
            this.DialogResult = DialogResult.OK;
        }

        private void CollocateFilterMetricDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (CollocateFilterMetricDropdown.Text)
            {
                case "NPMI":
                    CollocateFilterParameterNumericUpDown.Value = (decimal)0.5;
                    break;
                case "logDice":
                    CollocateFilterParameterNumericUpDown.Value = (decimal)8.0;
                    break;
            }

            
        }
    }
}
