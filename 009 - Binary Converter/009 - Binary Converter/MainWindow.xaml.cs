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
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace _009___Binary_Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // used long because conversion to binary string is easier for that than ulong
        // however, it'd be really good to be able to put in an arbitrarily large number and get the correct output
        // put that as a stretch goal, and if I can't achieve it just put an error message for when the input value is too great

        // input variables
        private long? DecimalInput;
        private long? BinaryInput;
        // value variables (used to compare against input and to calculate output)
        private long DecimalValue;
        private long BinaryValue;
        private string ConversionDirection; 


        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorText.Text = ""; // clear any previous error messages

            ReceiveInput(); // assigns input value to input variables

            DecideConversionDirection(); // determines direction of conversion (or error code if invalid input)
            AssignCalculationValues(); // 

            if (ConversionDirection == "NoInput") // Error Message for no input on either
                ErrorText.Text = "*** There is no value to convert. Please enter a number in one of the text boxes.";
            else if (ConversionDirection == "DoubleInput") // Error message for double input
                ErrorText.Text = "*** Cannot convert two values at once. Please convert one at a time";
            else
            {
                if (ConversionDirection == "DecToBin")
                {
                    string BinaryOutput = Convert.ToString(DecimalValue, 2);
                    BinaryBox.Text = BinaryOutput;
                }
                else
                {
                    long output = Convert.ToInt64(Convert.ToString(BinaryValue), 2);
                    DecimalBox.Text = Convert.ToString(output);
                }

            }
         }

        private void ReceiveInput()
        {
            DecimalInput = Int64.TryParse(DecimalBox.Text, out long decResult) ? (long?)decResult : null;
            BinaryInput = Int64.TryParse(BinaryBox.Text, out long binResult) ? (long?)binResult : null;

            Debug.WriteLine("The following inputs were received:");
            Debug.WriteLine(String.Format("DecimalInput: {0}", DecimalInput));
            Debug.WriteLine(String.Format("BinaryInput: {0}", BinaryInput));
        }

        private void DecideConversionDirection() // sets ConversionDirection based on input. In case of invalid input, returns error codes.
        {
            ConversionDirection = null;
            if (DecimalInput == null && BinaryInput == null) // no input
                ConversionDirection = "NoInput";
            else if (DecimalInput != null && BinaryInput != null) // double input
                ConversionDirection = "DoubleInput";
            else if (DecimalInput != null) // decimal input
                ConversionDirection = "DecToBin";
            else if (BinaryInput != null) // binary input
                ConversionDirection = "BinToDec";

            Debug.WriteLine(String.Format("At DecideConversionDirection, the assigned value was: {0}", ConversionDirection));
        }

        private void AssignCalculationValues() // checks which input has been provided, then assigns that input to appropriate Value variable
        {
            DecimalValue = (Convert.ToInt64(DecimalInput) != DecimalValue) ? Convert.ToInt64(DecimalInput) : DecimalValue;
            BinaryValue = (Convert.ToInt64(BinaryInput) != BinaryValue) ? Convert.ToInt64(BinaryInput) : BinaryValue;

            Debug.WriteLine("At AssignAnswerValue, the following values were assigned:");
            Debug.WriteLine(String.Format("DecimalValue: {0}", DecimalValue));
            Debug.WriteLine(String.Format("BinaryValue: {0}", BinaryValue));
        }

        private void DecimalBox_PreviewTextInput(object sender, TextCompositionEventArgs e) // only allows integer input to decimal box
        {
            e.Handled = !IsTextAllowed(e.Text, _decimalRegex);
        }

        private void BinaryBox_PreviewTextInput(object sender, TextCompositionEventArgs e) // only allows binary input to binary box
        {
            e.Handled = !IsTextAllowed(e.Text, _binaryRegex);
        }

        private void DecimalBox_Pasting(object sender, DataObjectPastingEventArgs e) // prevents pasting of invalid text into decimal box
        {
            if (!IsPasteValid(e, _decimalRegex))
                e.CancelCommand();
        }

        private void BinaryBox_Pasting(object sender, DataObjectPastingEventArgs e) // prevents pasting of invalid text into binary box
        {
            if (!IsPasteValid(e, _binaryRegex))
                e.CancelCommand();
        }

        private static readonly Regex _decimalRegex = new Regex("^[0-9]+$"); // used to check if decimal input is integer
        private static readonly Regex _binaryRegex = new Regex("^[0-1]+$"); // used to check if binary input is 0 or 1

        private bool IsTextAllowed(string text, Regex regex) // returns true if text matches regex
        {
            return regex.IsMatch(text);
        }

        private bool IsPasteValid(DataObjectPastingEventArgs e, Regex regex) //returns true if pasted text matches regex
        {
            string text = (String)e.DataObject.GetData(typeof(String));
            return IsTextAllowed(text, regex);
        }

    }
}
