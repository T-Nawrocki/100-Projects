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

namespace e_Finder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // change this to change max decimal places
        // do not exceed 15, because Math.Round only takes values up to 15
        const double E = Math.E;
        const int MaxDigits = 10;

        public MainWindow()
        {
            InitializeComponent();

            // sets maximum input number and displays that maximum in instructions
            InputBox.Maximum = MaxDigits;
            MaxDigitsText.Text = MaxDigits.ToString();

        }

        // sets output text on button click
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            int Input = Convert.ToInt16(InputBox.Value);
            OutputText.Text = string.Format(
                "The value of e to {0} decimal places is: {1}", 
                Input.ToString(),
                Convert.ToString(Math.Round(E, Input))
                );
        }

    }
}
