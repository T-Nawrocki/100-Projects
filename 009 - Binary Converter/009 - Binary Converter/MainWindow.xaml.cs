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

namespace _009___Binary_Converter
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

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            //declare two container variables
            // if one input value != container
                // set container to input value
                // convert input value
                // set other container to output value
            // if both input value != container
                // throw error telling them to convert one at a time, please delete one.
            // if no input value
                // throw error telling them to input a value
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
