using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Currency_Converter_GUI {

    /// <summary>
    /// 
    /// This class manages the form and controls 
    /// the flow of the user's input to convert
    /// currency from one to another. Optionally
    /// restarts or exits the application.
    /// 
    /// Author: Matthew Price
    /// Student Number: ########
    /// Date: 2/05/2017
    /// 
    /// </summary>

    public partial class Form1 : Form {

        // Initialise variables for later use
        private int currencyHave_index;
        private int currencyWant_index;
        private double currencyWantConverted;

        /// <summary>
        /// Opens the form
        /// </summary>
        public Form1() {
            InitializeComponent();
        }//end Form1

        /// <summary>
        /// Load the form and initialise the comboBoxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // For each combo box, initialise it
            comboBox_CurrencyHave.Items.AddRange(Currency_Exchange_Class.InitialiseComboBox());
            comboBox_CurrencyWant.Items.AddRange(Currency_Exchange_Class.InitialiseComboBox());
        } // End Form1_Load()

        /// <summary>
        /// When the user selects a currency to convert from, disable that element and enable selection of the currency to convert to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_CurrencyHave_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected currency to convert from, store for use in conversion
            currencyHave_index = comboBox_CurrencyHave.SelectedIndex;

            // Get the currency code, and display it next to the amount entry box
            Currencies currencyHave = (Currencies)currencyHave_index;
            Label_CountryCode_Have.Text = currencyHave.ToString();
            Label_CountryCode_Have.Visible = true;

            // Enable convert to selection box
            comboBox_CurrencyHave.Enabled = false;
            comboBox_CurrencyWant.Enabled = true;
        } // End comboBox_CurrencyHave_SelectedIndexChanged()

        /// <summary>
        /// When the user selects a currency to convert to, disable that element and enable amount entry.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_CurrencyWant_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected currency to convert to, store for use in conversion
            currencyWant_index = comboBox_CurrencyWant.SelectedIndex;

            // Get the currency code, and display it next to the converted amount box
            Currencies currencyWant = (Currencies)currencyWant_index;
            Label_CountryCode_Want.Text = currencyWant.ToString();
            Label_CountryCode_Want.Visible = true;

            // Disable this element
            comboBox_CurrencyWant.Enabled = false;

            // Enable amount entry and conversion button
            richTextBox_AmountHave.Enabled = true;
            Button_Equals.Enabled = true;
        } // End comboBox_CurrencyWant_SelectedIndexChanged()

        /// <summary>
        /// When the user clicks the button, convert and display currency according to the chosen currencies.
        /// Then display end of program options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Equals_Click(object sender, EventArgs e)
        {
            // Convert Currency
            currencyWantConverted = Currency_Exchange_Class.ConvertCurrency(currencyHave_index, currencyWant_index, richTextBox_AmountHave.Text);

            // If the convertCurrency succeeded
            if (currencyWantConverted >= 0) {
                // Display the converted amount then disable amount input and convert button
                richTextBox_AmountWant.Text = currencyWantConverted.ToString();
                richTextBox_AmountHave.Enabled = false;
                Button_Equals.Enabled = false;
                // Display the end of program options
                GroupBox_RadioButtons_Retry.Enabled = true;
                GroupBox_RadioButtons_Retry.Visible = true;
            } else {
                // If the convertCurrency failed, show the user an error message
                DialogResult ValueError = MessageBox.Show("Please input positive numbers only.",
                                                              "Input Error", MessageBoxButtons.RetryCancel);
            }
        } // End Button_Equals_Click()

        /// <summary>
        /// If the user selects convert again, reset all components of the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_ConvertAgain_CheckedChanged(object sender, EventArgs e)
        {
            // Reset all components manually instead of Application.Restart for a better user experience
            // CurrencyHave related
            comboBox_CurrencyHave.SelectedIndex = 0;
            comboBox_CurrencyHave.Enabled = true;
            // CurrencyWant related
            comboBox_CurrencyWant.SelectedIndex = 0;
            comboBox_CurrencyWant.Enabled = false;
            // AmountHave related
            Label_CountryCode_Have.Visible = false;
            richTextBox_AmountHave.Text = null;
            richTextBox_AmountHave.Enabled = false;
            // Convert button
            Button_Equals.Enabled = false;
            // AmountWant related
            Label_CountryCode_Want.Visible = false;
            richTextBox_AmountWant.Text = null;
            // End of program related
            radioButton_ConvertAgain.Checked = false;
            GroupBox_RadioButtons_Retry.Visible = false;
            GroupBox_RadioButtons_Retry.Enabled = false;
        } // End radioButton_ConvertAgain_CheckedChanged()

        /// <summary>
        /// If the user selects no to convert again, creates a popup to confirm the user wanted to quit the program then quits.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioButton_Finished_CheckedChanged(object sender, EventArgs e)
        {
            // Create popup box, get answer
            DialogResult ExitResult = MessageBox.Show("Are you sure you would like to exit?",
                                                                  "Quit", MessageBoxButtons.YesNo);

            // If yes, close the program.
            if (ExitResult == DialogResult.Yes) {
                Application.Exit();
            }
        } // End radioButton_Finished_CheckedChanged()
    }//end class
}
