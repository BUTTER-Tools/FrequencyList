namespace FrequencyList
{
    partial class SettingsForm_FrequencyList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm_FrequencyList));
            this.OKButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.NgramBox = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MinObsPctNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.CollocateFilterMetricDropdown = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CollocateFilterParameterNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.minTokenCountBox = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.PruneIntervalUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.minFreqUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ReplaceSpaceCheckbox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.NgramBox)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinObsPctNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CollocateFilterParameterNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTokenCountBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PruneIntervalUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFreqUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // OKButton
            // 
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.Location = new System.Drawing.Point(150, 479);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(118, 40);
            this.OKButton.TabIndex = 6;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "N-grams, N = ";
            // 
            // NgramBox
            // 
            this.NgramBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NgramBox.Location = new System.Drawing.Point(251, 38);
            this.NgramBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NgramBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NgramBox.Name = "NgramBox";
            this.NgramBox.Size = new System.Drawing.Size(126, 25);
            this.NgramBox.TabIndex = 12;
            this.NgramBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NgramBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ReplaceSpaceCheckbox);
            this.groupBox1.Controls.Add(this.MinObsPctNumericUpDown);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.CollocateFilterMetricDropdown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CollocateFilterParameterNumericUpDown);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.minTokenCountBox);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.PruneIntervalUpDown);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.minFreqUpDown);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.NgramBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 447);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "N-Gram Settings";
            // 
            // MinObsPctNumericUpDown
            // 
            this.MinObsPctNumericUpDown.DecimalPlaces = 5;
            this.MinObsPctNumericUpDown.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinObsPctNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.MinObsPctNumericUpDown.Location = new System.Drawing.Point(251, 132);
            this.MinObsPctNumericUpDown.Name = "MinObsPctNumericUpDown";
            this.MinObsPctNumericUpDown.Size = new System.Drawing.Size(126, 25);
            this.MinObsPctNumericUpDown.TabIndex = 25;
            this.MinObsPctNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.MinObsPctNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(168, 32);
            this.label6.TabIndex = 24;
            this.label6.Text = "Omit n-grams occurring \r\nin < X% of documents";
            // 
            // CollocateFilterMetricDropdown
            // 
            this.CollocateFilterMetricDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CollocateFilterMetricDropdown.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CollocateFilterMetricDropdown.FormattingEnabled = true;
            this.CollocateFilterMetricDropdown.Location = new System.Drawing.Point(251, 288);
            this.CollocateFilterMetricDropdown.Name = "CollocateFilterMetricDropdown";
            this.CollocateFilterMetricDropdown.Size = new System.Drawing.Size(126, 26);
            this.CollocateFilterMetricDropdown.TabIndex = 23;
            this.CollocateFilterMetricDropdown.SelectedIndexChanged += new System.EventHandler(this.CollocateFilterMetricDropdown_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 16);
            this.label2.TabIndex = 22;
            this.label2.Text = "Filter collocates by";
            // 
            // CollocateFilterParameterNumericUpDown
            // 
            this.CollocateFilterParameterNumericUpDown.DecimalPlaces = 5;
            this.CollocateFilterParameterNumericUpDown.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CollocateFilterParameterNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.CollocateFilterParameterNumericUpDown.Location = new System.Drawing.Point(251, 342);
            this.CollocateFilterParameterNumericUpDown.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.CollocateFilterParameterNumericUpDown.Minimum = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.CollocateFilterParameterNumericUpDown.Name = "CollocateFilterParameterNumericUpDown";
            this.CollocateFilterParameterNumericUpDown.Size = new System.Drawing.Size(126, 25);
            this.CollocateFilterParameterNumericUpDown.TabIndex = 21;
            this.CollocateFilterParameterNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CollocateFilterParameterNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 337);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 32);
            this.label1.TabIndex = 20;
            this.label1.Text = "Filter out collocates where\r\nmetric is less than:";
            // 
            // minTokenCountBox
            // 
            this.minTokenCountBox.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minTokenCountBox.Location = new System.Drawing.Point(251, 240);
            this.minTokenCountBox.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.minTokenCountBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minTokenCountBox.Name = "minTokenCountBox";
            this.minTokenCountBox.Size = new System.Drawing.Size(126, 25);
            this.minTokenCountBox.TabIndex = 18;
            this.minTokenCountBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.minTokenCountBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 243);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(184, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Skip texts with < X tokens";
            // 
            // PruneIntervalUpDown
            // 
            this.PruneIntervalUpDown.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PruneIntervalUpDown.Increment = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.PruneIntervalUpDown.Location = new System.Drawing.Point(251, 189);
            this.PruneIntervalUpDown.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.PruneIntervalUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PruneIntervalUpDown.Name = "PruneIntervalUpDown";
            this.PruneIntervalUpDown.Size = new System.Drawing.Size(126, 25);
            this.PruneIntervalUpDown.TabIndex = 16;
            this.PruneIntervalUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PruneIntervalUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(221, 32);
            this.label4.TabIndex = 15;
            this.label4.Text = "Prune Interval (i.e., prune the\r\nfreq. list after every X n-grams)";
            // 
            // minFreqUpDown
            // 
            this.minFreqUpDown.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minFreqUpDown.Location = new System.Drawing.Point(251, 83);
            this.minFreqUpDown.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.minFreqUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minFreqUpDown.Name = "minFreqUpDown";
            this.minFreqUpDown.Size = new System.Drawing.Size(126, 25);
            this.minFreqUpDown.TabIndex = 14;
            this.minFreqUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.minFreqUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Omit n-grams with frequency <";
            // 
            // ReplaceSpaceCheckbox
            // 
            this.ReplaceSpaceCheckbox.AutoSize = true;
            this.ReplaceSpaceCheckbox.Font = new System.Drawing.Font("MS Reference Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReplaceSpaceCheckbox.Location = new System.Drawing.Point(19, 401);
            this.ReplaceSpaceCheckbox.Name = "ReplaceSpaceCheckbox";
            this.ReplaceSpaceCheckbox.Size = new System.Drawing.Size(362, 20);
            this.ReplaceSpaceCheckbox.TabIndex = 26;
            this.ReplaceSpaceCheckbox.Text = "Delimit N-Gram Tokens with Underscores in Output";
            this.ReplaceSpaceCheckbox.UseVisualStyleBackColor = true;
            // 
            // SettingsForm_FrequencyList
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 533);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm_FrequencyList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Read .txt Files from Folder Settings";
            ((System.ComponentModel.ISupportInitialize)(this.NgramBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MinObsPctNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CollocateFilterParameterNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTokenCountBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PruneIntervalUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minFreqUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown NgramBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown minFreqUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown PruneIntervalUpDown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown minTokenCountBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown CollocateFilterParameterNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CollocateFilterMetricDropdown;
        private System.Windows.Forms.NumericUpDown MinObsPctNumericUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ReplaceSpaceCheckbox;
    }
}