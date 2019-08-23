using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _004___Prime_Factors
{
    public partial class MainWindow : Window
    {
        public List<int> Factors = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // resets everything to have a clean start
                Reset();

                // calculates prime factors
                CalculatePrimeFactors(Convert.ToInt64(InputBox.Text));

                // sets output (with special case if input is prime)
                if (Factors.Count() == 1)
                    OutputText.Text = String.Format("{0} is prime.", InputBox.Text);
                else
                {
                    OutputText.Text = String.Format("The prime factors of {0} are:\n", InputBox.Text);
                    OutputText.Text += String.Join(" * ", Factors);
                }
                
            }
        }

        // resets list of factors and output text
        private void Reset()
        {
            Factors.Clear();
            OutputText.Text = "";
        }

        // calculates prime factors
        private void CalculatePrimeFactors(long input)
        {
            int CurrentPrime = 2;
            while (input > 1) // if input = 1, it has no more factors
            {
                // if input is divisible by current prime, add current prime to list, and divide input by current prime.
                if (input % CurrentPrime == 0)  
                {
                    Factors.Add(CurrentPrime);
                    input /= CurrentPrime;
                }
                // else increment—this will continue until the next factor is found
                else
                    CurrentPrime++;
            }
        }
    }
}
