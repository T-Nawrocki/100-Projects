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
        private static readonly Regex _inputRegex = new Regex(@"^\d+\.?\d*$"); // regex for valid input (any number of digits; up to one .; any number of digits)

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            // change colour to highlight current mouse focus
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            // return colour to default (depending on style) after mouse focus leaves
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // change colour or add border to highlight mouse down
        }

        private void Button_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // return colour and border to default (depending on style) after mouse up
        }

        private void IODisplay_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // restricts typed input to valid characters only
            if (e.Text != ".")
                e.Handled = !_inputRegex.IsMatch(e.Text); // for all characters other than ".", check regex to see if valid
            else
                e.Handled = IODisplay.Text.Contains("."); // for ".", permit only one
        }

        private void IODisplay_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            // restricts pasted input to valid characters only
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // master event handler for button clicks. 
            // Might make sense to split this into separate handlers for each type of button (maybe one for numberbuttons, and one each for the others)
        }
    }
}
