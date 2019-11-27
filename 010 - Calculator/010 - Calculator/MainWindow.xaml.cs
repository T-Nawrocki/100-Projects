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
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace _010___Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Data Validation

        private static readonly Regex _inputRegex = new Regex(@"^\d+\.?\d*$"); // regex for valid input (any number of digits; up to one .; any number of digits)

        private void IODisplay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // restricts typed input to valid characters only
            if (e.Text != ".")
                e.Handled = !_inputRegex.IsMatch(e.Text); // for all characters other than ".", check regex to see if valid
            else
                e.Handled = IODisplay.Text.Contains("."); // for ".", permit only one
        }

        private void IODisplay_LostFocus(object sender, RoutedEventArgs e)
        {
            CleanUpInput(); // run cleanup input method
        }

        private void IODisplay_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            // restricts pasted input to valid characters only
            string input = (String)e.DataObject.GetData(typeof(String)); // gets pasted data and assigns it to string

            if (!_inputRegex.IsMatch(input)) // cancels paste if input doesn't match regex
                e.CancelCommand();

            if (input.Contains(".") && IODisplay.Text.Contains(".")) // cancels paste if pasted data would result in two decimal points
                e.CancelCommand();
        }

        private void CleanUpInput()
        {
            // tidies up formatting of input by deleting redundant 0 and . characters and adding a 0 before initial .
            string input = IODisplay.Text;

            if (input.Contains("."))
            {
                // adds 0 before initial .
                if (input[0] == '.')
                    input = "0" + input;

                input = input.TrimEnd('0'); // removes final 0 after a decimal point

                input = input.TrimEnd('.'); // removes final .
            }

            // removes initial 0 until length = 1 or the second character is .
            while (input.Length > 1 && input[0] == '0' && input[1] != '.')
                input = input.Remove(0, 1);

            IODisplay.Text = input;

        }



        #endregion

        #region Reset Button Event Handlers
        private void ClearInputButton_Click(object sender, RoutedEventArgs e)
        {
            IODisplay.Text = "";
        }

        private void ClearAllButton_Click(object sender, RoutedEventArgs e)
        {
            IODisplay.Text = "";
            CurrentSumDisplay.Content = ""; // NEEDS TO BE SET BACK TO NULL, NOT EMPTY STRING
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IODisplay.Text = IODisplay.Text.Remove(IODisplay.Text.Length - 1);
        }
        #endregion

        #region Basic Maths Button Event Handlers
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            IODisplay.Text += (sender as Button).Content;
        }

        private void DecimalButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IODisplay.Text.Contains("."))
                IODisplay.Text += ".";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            CleanUpInput();

            if (IODisplay.Text != "") // if there's no input, we don't want to do anything
            {
                if (CurrentSumDisplay.Content == null) // if there's no current sum, push input and "+" to current sum
                {
                    CurrentSumDisplay.Content += IODisplay.Text + " " + (sender as Button).Content + " "; //USE THIS FOR ALL OF THEM SINGLE EVENT HANDLER YOU DID IT WOO
                    IODisplay.Text = ""; // resets input to empty string, ready for next input
                }
            }
            else
                throw new NotImplementedException(); // still have to do something about when the user presses + but there's already an active sum
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
            CleanUpInput();

            if (IODisplay.Text != "") // if there's no input, we don't want to do anything
            {
                if (CurrentSumDisplay.Content == null) // if there's no current sum, push input and "-" to current sum
                {
                    CurrentSumDisplay.Content += IODisplay.Text + " - ";
                    IODisplay.Text = ""; // resets input to empty string, ready for next input
                }
            }
            else
                throw new NotImplementedException(); // still have to do something about when the user presses + but there's already an active sum
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
            CleanUpInput();

            if (IODisplay.Text != "") // if there's no input, we don't want to do anything
            {
                if (CurrentSumDisplay.Content == null) // if there's no current sum, push input and "x" to current sum
                {
                    CurrentSumDisplay.Content += IODisplay.Text + " \u00D7 ";
                    IODisplay.Text = ""; // resets input to empty string, ready for next input
                }
            }
            else
                throw new NotImplementedException(); // still have to do something about when the user presses x but there's already an active sum
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            CleanUpInput();

            if (IODisplay.Text != "") // if there's no input, we don't want to do anything
            {
                if (CurrentSumDisplay.Content == null) // if there's no current sum, push input and "x" to current sum
                {
                    CurrentSumDisplay.Content += IODisplay.Text + " \u00F7 ";
                    IODisplay.Text = ""; // resets input to empty string, ready for next input
                }
            }
            else
                throw new NotImplementedException(); // still have to do something about when the user presses x but there's already an active sum
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion

        #region Complex Maths Button Event Handlers
        private void SignButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RootButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SquaredButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FractionButton_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
