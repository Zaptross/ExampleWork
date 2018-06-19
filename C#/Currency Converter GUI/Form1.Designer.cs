namespace Currency_Converter_GUI {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Label_ComboBox_Have = new System.Windows.Forms.Label();
            this.comboBox_CurrencyHave = new System.Windows.Forms.ComboBox();
            this.Button_Equals = new System.Windows.Forms.Button();
            this.radioButton_ConvertAgain = new System.Windows.Forms.RadioButton();
            this.GroupBox_RadioButtons_Retry = new System.Windows.Forms.GroupBox();
            this.radioButton_Finished = new System.Windows.Forms.RadioButton();
            this.richTextBox_AmountHave = new System.Windows.Forms.RichTextBox();
            this.comboBox_CurrencyWant = new System.Windows.Forms.ComboBox();
            this.Label_ComboBox_Want = new System.Windows.Forms.Label();
            this.Label_RichTextEntry_Have = new System.Windows.Forms.Label();
            this.Label_RichTextEntry_Want = new System.Windows.Forms.Label();
            this.richTextBox_AmountWant = new System.Windows.Forms.RichTextBox();
            this.DEBUG_LABEL = new System.Windows.Forms.Label();
            this.Label_CountryCode_Have = new System.Windows.Forms.Label();
            this.Label_CountryCode_Want = new System.Windows.Forms.Label();
            this.GroupBox_RadioButtons_Retry.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_ComboBox_Have
            // 
            this.Label_ComboBox_Have.AutoSize = true;
            this.Label_ComboBox_Have.Location = new System.Drawing.Point(21, 9);
            this.Label_ComboBox_Have.Name = "Label_ComboBox_Have";
            this.Label_ComboBox_Have.Size = new System.Drawing.Size(84, 13);
            this.Label_ComboBox_Have.TabIndex = 0;
            this.Label_ComboBox_Have.Text = "Currency I Have";
            // 
            // comboBox_CurrencyHave
            // 
            this.comboBox_CurrencyHave.FormattingEnabled = true;
            this.comboBox_CurrencyHave.Location = new System.Drawing.Point(24, 25);
            this.comboBox_CurrencyHave.Name = "comboBox_CurrencyHave";
            this.comboBox_CurrencyHave.Size = new System.Drawing.Size(121, 21);
            this.comboBox_CurrencyHave.TabIndex = 1;
            this.comboBox_CurrencyHave.SelectedIndexChanged += new System.EventHandler(this.comboBox_CurrencyHave_SelectedIndexChanged);
            // 
            // Button_Equals
            // 
            this.Button_Equals.Enabled = false;
            this.Button_Equals.Location = new System.Drawing.Point(174, 101);
            this.Button_Equals.Name = "Button_Equals";
            this.Button_Equals.Size = new System.Drawing.Size(75, 52);
            this.Button_Equals.TabIndex = 2;
            this.Button_Equals.Text = "Equals";
            this.Button_Equals.UseVisualStyleBackColor = true;
            this.Button_Equals.Click += new System.EventHandler(this.Button_Equals_Click);
            // 
            // radioButton_ConvertAgain
            // 
            this.radioButton_ConvertAgain.AutoSize = true;
            this.radioButton_ConvertAgain.Location = new System.Drawing.Point(52, 19);
            this.radioButton_ConvertAgain.Name = "radioButton_ConvertAgain";
            this.radioButton_ConvertAgain.Size = new System.Drawing.Size(43, 17);
            this.radioButton_ConvertAgain.TabIndex = 3;
            this.radioButton_ConvertAgain.TabStop = true;
            this.radioButton_ConvertAgain.Text = "Yes";
            this.radioButton_ConvertAgain.UseVisualStyleBackColor = true;
            this.radioButton_ConvertAgain.CheckedChanged += new System.EventHandler(this.radioButton_ConvertAgain_CheckedChanged);
            // 
            // GroupBox_RadioButtons_Retry
            // 
            this.GroupBox_RadioButtons_Retry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.GroupBox_RadioButtons_Retry.Controls.Add(this.radioButton_Finished);
            this.GroupBox_RadioButtons_Retry.Controls.Add(this.radioButton_ConvertAgain);
            this.GroupBox_RadioButtons_Retry.Enabled = false;
            this.GroupBox_RadioButtons_Retry.Location = new System.Drawing.Point(135, 202);
            this.GroupBox_RadioButtons_Retry.Name = "GroupBox_RadioButtons_Retry";
            this.GroupBox_RadioButtons_Retry.Size = new System.Drawing.Size(152, 72);
            this.GroupBox_RadioButtons_Retry.TabIndex = 4;
            this.GroupBox_RadioButtons_Retry.TabStop = false;
            this.GroupBox_RadioButtons_Retry.Text = "Another Conversion?";
            this.GroupBox_RadioButtons_Retry.Visible = false;
            // 
            // radioButton_Finished
            // 
            this.radioButton_Finished.AutoSize = true;
            this.radioButton_Finished.Location = new System.Drawing.Point(52, 42);
            this.radioButton_Finished.Name = "radioButton_Finished";
            this.radioButton_Finished.Size = new System.Drawing.Size(39, 17);
            this.radioButton_Finished.TabIndex = 4;
            this.radioButton_Finished.TabStop = true;
            this.radioButton_Finished.Text = "No";
            this.radioButton_Finished.UseVisualStyleBackColor = true;
            this.radioButton_Finished.CheckedChanged += new System.EventHandler(this.radioButton_Finished_CheckedChanged);
            // 
            // richTextBox_AmountHave
            // 
            this.richTextBox_AmountHave.Enabled = false;
            this.richTextBox_AmountHave.Location = new System.Drawing.Point(45, 117);
            this.richTextBox_AmountHave.Name = "richTextBox_AmountHave";
            this.richTextBox_AmountHave.Size = new System.Drawing.Size(100, 21);
            this.richTextBox_AmountHave.TabIndex = 0;
            this.richTextBox_AmountHave.Text = "";
            // 
            // comboBox_CurrencyWant
            // 
            this.comboBox_CurrencyWant.Enabled = false;
            this.comboBox_CurrencyWant.FormattingEnabled = true;
            this.comboBox_CurrencyWant.Location = new System.Drawing.Point(295, 25);
            this.comboBox_CurrencyWant.Name = "comboBox_CurrencyWant";
            this.comboBox_CurrencyWant.Size = new System.Drawing.Size(121, 21);
            this.comboBox_CurrencyWant.TabIndex = 6;
            this.comboBox_CurrencyWant.SelectedIndexChanged += new System.EventHandler(this.comboBox_CurrencyWant_SelectedIndexChanged);
            // 
            // Label_ComboBox_Want
            // 
            this.Label_ComboBox_Want.AutoSize = true;
            this.Label_ComboBox_Want.Location = new System.Drawing.Point(292, 9);
            this.Label_ComboBox_Want.Name = "Label_ComboBox_Want";
            this.Label_ComboBox_Want.Size = new System.Drawing.Size(84, 13);
            this.Label_ComboBox_Want.TabIndex = 5;
            this.Label_ComboBox_Want.Text = "Currency I Want";
            // 
            // Label_RichTextEntry_Have
            // 
            this.Label_RichTextEntry_Have.AutoSize = true;
            this.Label_RichTextEntry_Have.Location = new System.Drawing.Point(21, 101);
            this.Label_RichTextEntry_Have.Name = "Label_RichTextEntry_Have";
            this.Label_RichTextEntry_Have.Size = new System.Drawing.Size(78, 13);
            this.Label_RichTextEntry_Have.TabIndex = 7;
            this.Label_RichTextEntry_Have.Text = "Amount I Have";
            // 
            // Label_RichTextEntry_Want
            // 
            this.Label_RichTextEntry_Want.AutoSize = true;
            this.Label_RichTextEntry_Want.Location = new System.Drawing.Point(292, 101);
            this.Label_RichTextEntry_Want.Name = "Label_RichTextEntry_Want";
            this.Label_RichTextEntry_Want.Size = new System.Drawing.Size(78, 13);
            this.Label_RichTextEntry_Want.TabIndex = 9;
            this.Label_RichTextEntry_Want.Text = "Amount I Want";
            // 
            // richTextBox_AmountWant
            // 
            this.richTextBox_AmountWant.Enabled = false;
            this.richTextBox_AmountWant.Location = new System.Drawing.Point(322, 117);
            this.richTextBox_AmountWant.Name = "richTextBox_AmountWant";
            this.richTextBox_AmountWant.Size = new System.Drawing.Size(100, 21);
            this.richTextBox_AmountWant.TabIndex = 8;
            this.richTextBox_AmountWant.Text = "";
            // 
            // DEBUG_LABEL
            // 
            this.DEBUG_LABEL.AutoSize = true;
            this.DEBUG_LABEL.Location = new System.Drawing.Point(171, 63);
            this.DEBUG_LABEL.Name = "DEBUG_LABEL";
            this.DEBUG_LABEL.Size = new System.Drawing.Size(81, 13);
            this.DEBUG_LABEL.TabIndex = 10;
            this.DEBUG_LABEL.Text = "DEBUG LABEL";
            // 
            // Label_CountryCode_Have
            // 
            this.Label_CountryCode_Have.AutoSize = true;
            this.Label_CountryCode_Have.Location = new System.Drawing.Point(9, 121);
            this.Label_CountryCode_Have.Name = "Label_CountryCode_Have";
            this.Label_CountryCode_Have.Size = new System.Drawing.Size(30, 13);
            this.Label_CountryCode_Have.TabIndex = 11;
            this.Label_CountryCode_Have.Text = "COD";
            this.Label_CountryCode_Have.Visible = false;
            // 
            // Label_CountryCode_Want
            // 
            this.Label_CountryCode_Want.AutoSize = true;
            this.Label_CountryCode_Want.Location = new System.Drawing.Point(286, 121);
            this.Label_CountryCode_Want.Name = "Label_CountryCode_Want";
            this.Label_CountryCode_Want.Size = new System.Drawing.Size(30, 13);
            this.Label_CountryCode_Want.TabIndex = 12;
            this.Label_CountryCode_Want.Text = "COD";
            this.Label_CountryCode_Want.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 332);
            this.Controls.Add(this.Label_CountryCode_Want);
            this.Controls.Add(this.Label_CountryCode_Have);
            this.Controls.Add(this.DEBUG_LABEL);
            this.Controls.Add(this.Label_RichTextEntry_Want);
            this.Controls.Add(this.richTextBox_AmountWant);
            this.Controls.Add(this.Label_RichTextEntry_Have);
            this.Controls.Add(this.comboBox_CurrencyWant);
            this.Controls.Add(this.Label_ComboBox_Want);
            this.Controls.Add(this.richTextBox_AmountHave);
            this.Controls.Add(this.GroupBox_RadioButtons_Retry);
            this.Controls.Add(this.Button_Equals);
            this.Controls.Add(this.comboBox_CurrencyHave);
            this.Controls.Add(this.Label_ComboBox_Have);
            this.Name = "Form1";
            this.Text = "Currency Converter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupBox_RadioButtons_Retry.ResumeLayout(false);
            this.GroupBox_RadioButtons_Retry.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_ComboBox_Have;
        private System.Windows.Forms.ComboBox comboBox_CurrencyHave;
        private System.Windows.Forms.Button Button_Equals;
        private System.Windows.Forms.RadioButton radioButton_ConvertAgain;
        private System.Windows.Forms.GroupBox GroupBox_RadioButtons_Retry;
        private System.Windows.Forms.RadioButton radioButton_Finished;
        private System.Windows.Forms.RichTextBox richTextBox_AmountHave;
        private System.Windows.Forms.ComboBox comboBox_CurrencyWant;
        private System.Windows.Forms.Label Label_ComboBox_Want;
        private System.Windows.Forms.Label Label_RichTextEntry_Have;
        private System.Windows.Forms.Label Label_RichTextEntry_Want;
        private System.Windows.Forms.RichTextBox richTextBox_AmountWant;
        private System.Windows.Forms.Label DEBUG_LABEL;
        private System.Windows.Forms.Label Label_CountryCode_Have;
        private System.Windows.Forms.Label Label_CountryCode_Want;
    }
}

