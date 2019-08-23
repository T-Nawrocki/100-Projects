using System;
using System.Collections.Generic;
using System.Windows;
using System.Numerics;

namespace _003___Fibonacci
{
    public partial class MainWindow : Window
    {

        public BigInteger CurrentNumber = 1;
        public List<BigInteger> Sequence = new List<BigInteger>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateToMaxButton_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            int Input = Convert.ToInt32(InputBox.Value);

            // loops until i is greater than the input maximum
            while (CurrentNumber <= Input)
            {
                GenerateSequence();
            }

            OutputText.Text = string.Join("\n", Sequence);
        }

        private void GenerateNPlacesButton_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            int Input = Convert.ToInt32(InputBox.Value);

            // loops until Sequence.Count = input
            while (Sequence.Count < Input)
            {
                GenerateSequence();
            }

            OutputText.Text = string.Join("\n", Sequence);
        }

        // generates the fibonacci sequence in the Sequence list
        private void GenerateSequence()
        {
            Sequence.Add(CurrentNumber);
            if (Sequence.Count != 1)
                CurrentNumber += Sequence[(Sequence.Count - 2)];
        }

        // resets everything
        private void Reset()
        {
            CurrentNumber = 1;
            Sequence.Clear();
            OutputText.Text = "";
        }
    }
}
