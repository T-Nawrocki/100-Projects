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

namespace _007___Mortgage_Calculator
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            OutputText.Text = ""; //resets output text
            
            // checks if questions are unanswered—only continues if all questions are answered
            if (! IsAnyQuestionUnanswered())
            {

                int InitialDebt = Convert.ToInt32(TotalDebtInput.Value);
                int Term = Convert.ToInt32(TermInput.Value * 365.25); // term input is years, processed as days
                double InterestRate = Convert.ToSingle(InterestInput.Value);

                // Using substring to remove Interest/Pay from start of Interest/Payment Period, leaving only "Daily" etc
                string InterestPeriodWords = Convert.ToString(InterestPeriodSelect.SelectedItem).Substring(8);
                string PaymentPeriodWords = Convert.ToString(PaymentPeriodSelect.SelectedItem).Substring(3);

                // converts interest and payment period selects into an equivalent value in days
                double InterestPeriod = AssignInterestPeriod();
                double PaymentPeriod = AssignPaymentPeriod();

                // calculate total including interest
                double TotalDebt = InitialDebt;
                if (InterestPeriod == 0) // fixed interest
                    TotalDebt += TotalDebt * InterestRate;
                else // compound interest
                {
                    double NumberOfInterestPeriods = Term / InterestPeriod;

                    int i = 0;
                    while (i < Math.Floor(NumberOfInterestPeriods)) // first find total for whole interest periods
                    {
                        TotalDebt += InterestRate * TotalDebt;
                        i++;
                    }
                    // adds on last fraction of interest period (if whole number of interest periods, this will add 0)
                    TotalDebt += InterestRate * TotalDebt * (NumberOfInterestPeriods - Math.Floor(NumberOfInterestPeriods));
                }
                
                // calculate payment per payment period
                int NumberOfPaymentPeriods = Convert.ToInt32(Math.Round(Term / PaymentPeriod)); //can't have fraction of a payment period
                double PaymentAmount = TotalDebt / NumberOfPaymentPeriods;

                // sets output text
                OutputText.Text = String.Format("To pay back a £{0:n} mortgage over {1:n0} year", InitialDebt, TermInput.Value);
                if (TermInput.Value > 1)
                    OutputText.Text += "s";
                OutputText.Text += ", with a";
                if (InterestPeriod == 0)
                    OutputText.Text += String.Format(" fixed interest rate of {0:P}, ", InterestRate);
                else
                    OutputText.Text += String.Format(
                        "n interest rate of {0:P}, compounding {1}, ",
                        InterestRate,
                        InterestPeriodWords.ToLower());
                OutputText.Text += String.Format(
                    ", and paying on a {0} basis, will require {1:n0} payments of £{2:n}.",
                    PaymentPeriodWords.ToLower(),
                    NumberOfPaymentPeriods,
                    PaymentAmount);
            }
        }

        // if any question is unanswered, displays error message and returns true
        private bool IsAnyQuestionUnanswered()
        {
            if (TotalDebtInput.Value == null)
            {
                OutputText.Text = "Please input a total debt value and try again.";
                return true;
            }
            else if (InterestPeriodSelect.SelectedItem == null)
            {
                OutputText.Text = "Please select an interest period and try again.";
                return true;
            }
            else if (InterestInput.Value == null)
            {
                OutputText.Text = "Please input an interest rate and try again.";
                return true;
            }
            else if (TermInput.Value == null)
            {
                OutputText.Text = "Please input a term for the debt and try again.";
                return true;
            }
            else if (PaymentPeriodSelect.SelectedItem == null)
            {
                OutputText.Text = "Please select a payment period and try again.";
                return true;
            }
            else
                return false;
        }

        private double AssignInterestPeriod()
        {
            if (InterestPeriodSelect.SelectedItem == InterestDaily)
                return 1;
            else if (InterestPeriodSelect.SelectedItem == InterestWeekly)
                return 7;
            else if (InterestPeriodSelect.SelectedItem == InterestMonthly)
                return 365.25 / 12;
            else if (InterestPeriodSelect.SelectedItem == InterestAnnual)
                return 365.25;
            else
                return 0; // used for fixed interest, which doesn't rely on period being set
        }

        private double AssignPaymentPeriod()
        {
            if (PaymentPeriodSelect.SelectedItem == PayDaily)
                return 1;
            else if (PaymentPeriodSelect.SelectedItem == PayWeekly)
                return 7;
            else if (PaymentPeriodSelect.SelectedItem == PayMonthly)
                return 365.25 / 12;
            else
                return 365.25;
        }
    }
}
