using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Currency_Converter_GUI {

    /// <summary>
    /// 
    /// This class enumerates currency codes, exchange rates, 
    /// country lists, and manages conversion of currencies.
    /// 
    /// Author: Matthew Price
    /// Student Number: ########
    /// Date: 2/05/2017
    /// 
    /// </summary>

    // Enumerates currency codes
    enum Currencies { AUD = 1, CYN, DKK, EUR, INR, NZD, AED, GBP, USD, VND };

    static class Currency_Exchange_Class {

        // Double array containing exchange rates
        private static double[] xRates = { 0, 1, 4.2681, 5.0844, 0.6849, 43.5921, 0.9705, 2.7094, 0.4963, 0.7382, 19115.5547 };


        /// <summary>
        /// Provides country names and currency code which  can be used to initialise a Combo Box
        /// </summary>
        /// <returns> string array each element of which contains the country name and three letter currency code</returns>
        public static string[] InitialiseComboBox() {

            string[] countries = {   "",
                                    "Australia (AUD)",
                                    "China (CNY)",
                                    "Denmark (DKK)",
                                    "Europe (EUR)",
                                    "India (INR)",
                                    "New Zealand (NZD)",
                                    "United Arab Emirates (AED)",
                                    "United Kingdom (GBP)",
                                    "United States (USD)",
                                    "Vietnam (VND)" };

            return countries;
        } //end InitialiseComboBox()             

        /// <summary>
        /// Convert and return an amount, from one currency to another currency.
        /// </summary>
        /// <param name="currencyHaveIndex">Index of currency to convert from</param>
        /// <param name="currencyWantIndex">Index of currency to convert to</param>
        /// <param name="amountHave">Amount of currency to convert </param>
        /// <returns>Converted amount of currency</returns>
        public static double ConvertCurrency(int currencyHaveIndex, int currencyWantIndex, string amountHave) {
            // Initialise variables
            double exchangeRateHave;
            double exchangeRateWant;
            double amountHaveDouble;

            // Initialise in an error state
            double amountWantOut = -1.0d;

            // Get the exchange rates that matches the currency indicies
            exchangeRateHave = xRates[currencyHaveIndex];
            exchangeRateWant = xRates[currencyWantIndex];

            // If the amount does parse as a double, convert it from one currency to the other
            if (double.TryParse(amountHave, out amountHaveDouble)) {
                // Convert to AUD, then convert to WANTED currency
                amountWantOut = (amountHaveDouble / exchangeRateHave) * exchangeRateWant;
            }

            return amountWantOut;
        } // End ConvertCurrency()

    }//end class
}
