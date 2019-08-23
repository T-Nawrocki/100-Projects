using System;
using System.Collections.Generic;
using System.Windows;

namespace _003___Fibonacci
{
    public partial class MainWindow : Window
    {
        /* TO DO:
         * refactor to combine button event handlers
         * handle integer overflow issues when you get to big numbers
         */


        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateToMaxButton_Click(object sender, RoutedEventArgs e)
        {
            int Input = Convert.ToInt32(InputBox.Value);
            // i is "current number" in sequence, starts at 1.
            int i = 1;
            // declares new list of ints (indeterminate initial length)
            List<int> Sequence = new List<int>();

            // loops until i is greater than the input maximum
            while (i <= Input)
            {
                Sequence.Add(i);
                if (Sequence.Count != 1)
                    i += Sequence[(Sequence.Count - 2)];
            }

            OutputText.Text = string.Join(", ", Sequence);
        }

        private void GenerateNPlacesButton_Click(object sender, RoutedEventArgs e)
        {
            int Input = Convert.ToInt32(InputBox.Value);
            // i is "current number" in sequence, starts at 1.
            int i = 1;
            // declares new list of ints (indeterminate initial length)
            List<int> Sequence = new List<int>();

            // loops until Sequence.Count = input
            while (Sequence.Count < Input)
            {
                Sequence.Add(i);
                if (Sequence.Count != 1)
                    i += Sequence[(Sequence.Count - 2)];
            }

            OutputText.Text = string.Join(", ", Sequence);
        }
    }
}
