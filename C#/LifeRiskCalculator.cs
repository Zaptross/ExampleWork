using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAB201Assignment1
{
    /// <summary>
    /// 
    /// Name: Matthew Price
    /// Student number: ########
    /// Date: 22 March 2017
    /// 
    /// This is a menu driven program which repeatedly
    /// compares height to waist measurements to provide
    /// users with a ratio indicating whether they are
    /// at risk of developing obesity or not.
    /// 
    /// </summary>

    class Program {

        //Declaring constant values
        const int QuitNowCode = 1;
        const float RiskRatioMale = 0.536f;
        const float RiskRatioFemale = 0.492f;
        const float RiskRatioMin = 0;
        const float RiskRatioMax = 1;
        const float MinimumHeight = 120.0f;
        const float MinimumWaist = 60.0f;
        const string QuitNow = "QUIT";
        const string GenderMale = "Male";
        const string GenderFemale = "Female";
        const string BoolYes = "Y";
        const string BoolNo = "N";
        const string BoolMale = "M";
        const string BoolFemale = "F";
        const string AskHeight = "\nPlease enter your height:";
        const string AskWaist = "\nPlease enter your waist measurment:";
        const string AskGender = "\nPlease enter your biological gender: (F / M)";
        const string AskTryAgain = "\n\nWould you like to enter a new set of data? (Y / N)";
        const string TextExit = "Thanks for using L.R.C.!\nPress any key to exit...";
        const string TextIntro = "Welcome to the Lifestyle Risk Calculator!";
        const string TextIntroQuit = "Quit at any time by entering: \"QUIT\"";
        const string TextInfoDisplay = "Your details:\n\nHeight: {0}\n\nWaist: {1}\n\nGender: {2}\n\n{2} risk threshold: {3}\n\nYour ratio: {4}";
        const string RiskLowText = "\nYou are at little liftstyle risk.";
        const string RiskHighText = "\nYou are at high liftstyle risk.";
        const string RiskExtremeText = "\nYou are at an extreme liftstyle risk.";
        const string RiskImpossibleText = "\nYou are... a cardboard cutout? \n\nAre your measurements in the same units?";

        //Declaring error values
        const float ErrorIntValue = -1;
        const string ErrorStrValue = "error";
        const string ErrorIntText = "Error: please input an integer or decimal value equal to or greater than {0}.";
        const string ErrorBoolText = "Error: please input only {0} or {1}.";
        
        
        static void Main() {
            //Set the default to run the program again
            bool runAgain = true;

            Console.WriteLine(TextIntro);

            // Main loop, repeats after asking user if they want to start again
            while (runAgain) {
                // Initialising and resetting main variables in error states
                float userHeight = ErrorIntValue;
                float userWaist = ErrorIntValue;
                float ratio = ErrorIntValue;
                float ratioThreshold = ErrorIntValue;
                string userInput = ErrorStrValue;
                string genderTitle = ErrorStrValue;
                bool runAgainSuccess = false;

                // Tells and reminds user of the command for force quitting
                Console.WriteLine(TextIntroQuit);

                // Ask user for height and check the in input is valid, otherwise loop
                while (userHeight == ErrorIntValue) {
                    userInput = GetInput(AskHeight);
                    userHeight = ParseFloat(userInput, MinimumHeight);
                }

                // Ask user for waist measurement and check the in input is valid, otherwise loop
                while (userWaist == ErrorIntValue) {
                    userInput = GetInput(AskWaist);
                    userWaist = ParseFloat(userInput, MinimumWaist);
                }

                // Ask user for gender and check the in input is valid, otherwise loop
                while (ratioThreshold == ErrorIntValue) {
                    ratioThreshold = GetGenderThreshold(AskGender, out genderTitle);
                }

                ratio = CalculateRiskRatio(userHeight, userWaist);

                // Writes the user's information to the console
                DisplayUserInformation(userHeight, userWaist, genderTitle, ratioThreshold, ratio);

                // Check that runAgain wasn't an error value; then run again or not based on input
                while (!runAgainSuccess) {
                    runAgain = CheckRunAgain(AskTryAgain, out runAgainSuccess);
                }

                Console.Clear();
            }

            // Display exiting text and wait for any keypress before exit
            Console.WriteLine(TextExit);
            Console.ReadKey();
        } // Main()


        /// <summary>
        /// 
        /// Prints question, reads user input, and returns it as a string.
        /// Allows user to quit at any input point with "QUIT".
        /// 
        /// </summary>
        /// <param name="question">Question to ask user to answer.</param>
        /// <returns></returns>
        static string GetInput(string question) {
            string inputString = ErrorStrValue;

            Console.WriteLine(question);

            inputString = Console.ReadLine();

            // Checks if user wants to quit immediately, if so clear the screen and write the exit text
            if (inputString == QuitNow) {
                Console.Clear();
                Console.WriteLine(TextExit);
                Console.ReadKey();
                Environment.Exit(QuitNowCode);
            }

            return inputString;
        } // GetInput()


        /// <summary>
        /// 
        /// Reads input string, converts to float if possible.
        /// Outputs float.
        /// 
        /// </summary>
        /// <param name="inputString">String to parse</param>
        /// <returns></returns>
        static float ParseFloat(string inputString, float minimumValue) {
            float outputFloat = ErrorIntValue;

            // Checks if string doesn't convert to a float, or is smaller than the minimum value, if so throw an error
            if (!(float.TryParse(inputString, out outputFloat) && outputFloat >= minimumValue)) {
                Console.WriteLine(ErrorIntText, minimumValue);
                outputFloat = ErrorIntValue;
            }

            return outputFloat;
        } // ParseFloat()


        /// <summary>
        /// 
        /// Uses height and waist measurements to calculate the user's risk ratio.
        /// Ratio = Waist / Height
        /// 
        /// </summary>
        /// <param name="heightInput">User's height</param>
        /// <param name="waistInput">User's waist</param>
        /// <returns>User's Risk Ratio</returns>
        static float CalculateRiskRatio(float heightInput, float waistInput) {
            float userRatio = ErrorIntValue;
            
            // Checks that divisor is greater than zero, then calculates ratio
            if (waistInput > 0) {
                userRatio = waistInput / heightInput;
                userRatio = Convert.ToSingle(Math.Round(Convert.ToDouble(userRatio), 3));
            }

            return userRatio;
        } // CalculateRiskRatio()


        /// <summary>
        /// 
        /// Reads user's boolean input, returns as a boolean and outputs another boolean to indicate success.
        /// 
        /// </summary>
        /// <param name="inputQuestion">Question to ask user via GetInput().</param>
        /// <returns></returns>
        static bool CheckRunAgain(string inputQuestion, out bool inputSuccess) {
            string inputTryAgain = GetInput(inputQuestion);

            bool runAgainSuccess = true;

            bool inputBool = true;

            inputSuccess = false;

            // Checks if the input letter matches any available choices, sets both to lowercase, if not throws error
            if (inputTryAgain.ToLower() == BoolYes.ToLower()) {
                inputSuccess = true;
            } else if (inputTryAgain.ToLower() == BoolNo.ToLower()) {
                inputBool = false;
                inputSuccess = true;
            } else {
                Console.WriteLine(ErrorBoolText, BoolYes, BoolNo);
            }

            // If user input is false and isn't an error, do not run again
            if (!inputBool && inputSuccess) {
                runAgainSuccess = false;
            }

            return runAgainSuccess;
        } // CheckRunAgain()


        /// <summary>
        /// 
        /// Reads user input, determines gender, and outputs correct ratio.
        /// 
        /// </summary>
        /// <param name="boolAsk">Question to ask user via GetInput()</param>
        /// <returns></returns>
        static float GetGenderThreshold(string boolAsk, out string genderTitle) {
            float genderRatio = ErrorIntValue;

            string inputGender = GetInput(boolAsk);

            genderTitle = ErrorStrValue;

            // Check the user's inputted gender, set ratio and gender titles to match user, if doesn't match throw error
            if (inputGender.ToLower() == BoolMale.ToLower()) {
                genderRatio = RiskRatioMale;
                genderTitle = GenderMale;
            } else if (inputGender.ToLower() == BoolFemale.ToLower()) {
                genderRatio = RiskRatioFemale;
                genderTitle = GenderFemale;
            } else {
                Console.WriteLine(ErrorBoolText, BoolMale, BoolFemale);
                genderRatio = ErrorIntValue;
            }

            return genderRatio;
        } // GetGender()

        /// <summary>
        /// 
        /// Displays user's information and risk ratio results.
        /// Outputs risk level string.
        /// 
        /// </summary>
        /// <param name="height">User's height</param>
        /// <param name="waist">User's waist measurement</param>
        /// <param name="genderTitle">User's gender title</param>
        /// <param name="genderThreshold">User's risk threshold</param>
        /// <param name="userRatio">User's risk ratio</param>
        static void DisplayUserInformation(float height, float waist, string genderTitle, float genderThreshold, float userRatio) {
            Console.Clear();

            // Displays user's information
            Console.WriteLine(TextInfoDisplay, height, waist, genderTitle, genderThreshold, userRatio);

            // Compares user's ratio to threshold, and prints correlating risk text: low, high, extreme, impossible
            if (userRatio < genderThreshold && userRatio != RiskRatioMin) {
                Console.WriteLine(RiskLowText);
            } else if (userRatio >= genderThreshold && userRatio != RiskRatioMax) {
                Console.WriteLine(RiskHighText);
            } else if (userRatio == RiskRatioMax) {
                Console.WriteLine(RiskExtremeText);
            } else {
                Console.WriteLine(RiskImpossibleText);
            }
        }
    }
}