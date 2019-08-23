using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace _005___Prime_Sequence
{
    public partial class MainWindow : Window
    {
        // start at 2 (lowest prime)
        int CurrentPrime = 2;

        public MainWindow()
        {
            InitializeComponent();
        }

        // on click, displays current prime (comma separated) then calculates next prime
        private void AddPrimeButton_Click(object sender, RoutedEventArgs e)
        {
            if (OutputText.Text == "")
                OutputText.Text = CurrentPrime.ToString();
            else
            {
                OutputText.Text += ", " + CurrentPrime.ToString();
            }

            CurrentPrime = NextPrime(CurrentPrime);
        }

        // resets everything to startup conditions
        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            OutputText.Text = "";
            CurrentPrime = 2;
        }

        // returns next prime
        private int NextPrime(int input)
        {
            int Result = input + 1; // lowest possible result is input + 1

            while (NumberOfFactors(Result) > 1) // if number of factors is 1, result is prime
                Result++;

            return Result;
        }

        // creates list of factors and then returns number of factors of int input
        private int NumberOfFactors(int input)
        {
            List<int> Factors = new List<int>();
            int CurrentFactor = 2;

            while (input > 1)
                if (input % CurrentFactor == 0)
                {
                    Factors.Add(CurrentFactor);
                    input /= CurrentFactor;
                }
                else
                {
                    CurrentFactor++;
                }

            return Factors.Count();
        }
    }
}
