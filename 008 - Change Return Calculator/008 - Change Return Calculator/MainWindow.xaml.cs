using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace _008___Change_Return_Calculator
{
    public partial class MainWindow : Window
    {
        private decimal Price;
        private decimal CashReceived;
        private decimal TotalChange;
        private Dictionary<string, int> ChangeInCoins = new Dictionary<string, int>() //dictionary with denomination as key, number required as value
        {
            {"£50 note", 0},
            {"£20 note", 0},
            {"£10 note", 0},
            {"£5 note", 0},
            {"£2 coin", 0},
            {"£1 coin", 0},
            {"50p coin", 0},
            {"20p coin", 0},
            {"10p coin", 0},
            {"5p coin", 0},
            {"2p coin", 0},
            {"penny", 0}
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            Reset(); // resets everything to 0 so each calculation is run fresh

            if (!AreAnyInputsNull()) // only continues if both inputs are answered, otherwise prints error message
            {
                Price = Convert.ToDecimal(PriceInput.Value);
                CashReceived = Convert.ToDecimal(CashReceivedInput.Value);

                if (CashReceived < Price) // if cash received is less than price, prints an error message with difference.
                    OutputText.Text = String.Format("The cash received is not enough! You need to be paid £{0:N0} more", (Price - CashReceived));
                else
                {
                    TotalChange = CashReceived - Price;

                    CalculateChangeInCoins();

                    OutputText.Text = GenerateOutput();
                }
            }
        }

        // generates output text using the keys/values of ChangeInCoins
        private string GenerateOutput()
        {
            string output = String.Format("Total change: £{0:N}. The most efficient way to provide this is with ", TotalChange);
            
            // creates a dictionary only containing the key/values that we actually want to print (the ones where value != 0)
            Dictionary<string, int> ChangeToBePrinted = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> denomination in ChangeInCoins.Where(x => x.Value != 0))
                ChangeToBePrinted.Add(denomination.Key, denomination.Value);

            //comma separates all but the last denomination to be printed
            foreach (KeyValuePair<string, int> denomination in ChangeToBePrinted.Take(ChangeToBePrinted.Count - 1))
                if (denomination.Value == 1)
                    output += String.Format("a {0}, ", denomination.Key);
                else
                    output += String.Format("{0} {1}s, ", NumberToWords(denomination.Value), denomination.Key);

            // removes final ", " and replaces with "and " if there's more than one denomination to list
            if (ChangeToBePrinted.Count > 1)
            {
                output = output.Remove(output.Length - 2);
                output += " and ";
            }

            // adds last key/value to the list, "and" separated, with final full stop
            if (ChangeToBePrinted.LastOrDefault().Value == 1)
                output += String.Format("a {0}.", ChangeToBePrinted.LastOrDefault().Key);
            else
                output += String.Format("{0} {1}s.", NumberToWords(ChangeToBePrinted.LastOrDefault().Value), ChangeToBePrinted.LastOrDefault().Key);

            return output;
        }

        // converts int to words. Source: https://stackoverflow.com/questions/2729752/converting-numbers-in-to-words-c-sharp
        public static string NumberToWords(int number)
        {
            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        // finds most efficient way of providing change by starting at largest denomination and working down, incrementing the appropriate value in ChangeInCoins
        private void CalculateChangeInCoins()
        {
            decimal changeRemaining = TotalChange;
            while (changeRemaining != 0) // when change remaining = 0, we've got all the change we need
            {
                while (changeRemaining >= 50)
                {
                    ChangeInCoins["£50 note"]++;
                    changeRemaining -= 50;
                }
                while (changeRemaining >= 20)
                {
                    ChangeInCoins["£20 note"]++;
                    changeRemaining -= 20;
                }
                while (changeRemaining >= 10)
                {
                    ChangeInCoins["£10 note"]++;
                    changeRemaining -= 10;
                }
                while (changeRemaining >= 5)
                {
                    ChangeInCoins["£5 note"]++;
                    changeRemaining -= 5;
                }
                while (changeRemaining >= 2)
                {
                    ChangeInCoins["£2 coin"]++;
                    changeRemaining -= 2;
                }
                while (changeRemaining >= 1)
                {
                    ChangeInCoins["£1 coin"]++;
                    changeRemaining -= 1;
                }
                while (changeRemaining >= 0.5M) // "M" suffix makes this a decimal rather than double. The more you know...
                {
                    ChangeInCoins["50p coin"]++;
                    changeRemaining -= 0.5M;
                }
                while (changeRemaining >= 0.2M)
                {
                    ChangeInCoins["20p coin"]++;
                    changeRemaining -= 0.2M;
                }
                while (changeRemaining >= 0.1M)
                {
                    ChangeInCoins["10p coin"]++;
                    changeRemaining -= 0.1M;
                }
                while (changeRemaining >= 0.05M)
                {
                    ChangeInCoins["5p coin"]++;
                    changeRemaining -= 0.05M;
                }
                while (changeRemaining >= 0.02M)
                {
                    ChangeInCoins["2p coin"]++;
                    changeRemaining -= 0.02M;
                }
                // if we get here and changeRemaining still != 0, then changeRemaining = 0.01, so no need for a loop
                if (changeRemaining == 0.01M)
                {
                    ChangeInCoins["penny"]++;
                    changeRemaining -= 0.01M;
                }
            }
        }

        // returns true and displays error message if either input is null, else returns false
        private bool AreAnyInputsNull()
        {
            if (CashReceivedInput.Value == null || PriceInput.Value == null)
            {
                OutputText.Text = "Please input both the item's price and the cash received, then try again.";
                return true;
            }
            else
                return false;
        }

        // resets all values back to 0 and clears output text
        private void Reset()
        {
            Price = 0;
            CashReceived = 0;
            TotalChange = 0;
            ChangeInCoins = new Dictionary<string, int>()
            {
                {"£50 note", 0},
                {"£20 note", 0},
                {"£10 note", 0},
                {"£5 note", 0},
                {"£2 coin", 0},
                {"£1 coin", 0},
                {"50p coin", 0},
                {"20p coin", 0},
                {"10p coin", 0},
                {"5p coin", 0},
                {"2p coin", 0},
                {"penny", 0}
            };
            OutputText.Text = "";
        }
    }
}
