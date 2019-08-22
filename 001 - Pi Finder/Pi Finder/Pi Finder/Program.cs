using System;

namespace Pi_Finder
{
    class Program
    {
        static void Main(string[] args)
        {
            const double Pi = Math.PI;
            // change this to change max decimal places
            // max DigitLimit is 15 (Math.Round() does not allow more)
            const int DigitLimit = 12;

            string Input;
            int Digits;

            // presents initial prompt showing digit limit and gets string input
            Console.WriteLine("This program generates pi to the number of decimal places you input (up to {0}).", DigitLimit);
            Input = GetInput();

            // requires positive whole number input below digit limit to continue
            // if input is not positive whole number, requests new input
            while (!IsDigitsOnly(Input))
            {
                Console.WriteLine("Please insert positive whole numbers only.");
                Input = GetInput();
            }
            while (Convert.ToInt16(Input) > DigitLimit)
            {
                Console.WriteLine("The maximum number of decimal places is {0}. Please try again.", DigitLimit);
                Input = GetInput();
            }

            // converts string input to int, then prints output line with Pi rounded to decimal places specified
            Digits = Convert.ToInt16(Input);
            Console.WriteLine("The value of pi to {0} decimal places is: {1}", Digits, Math.Round(Pi, Digits));
            Console.ReadLine();
        }

        // presents prompt to user and returns input as string
        public static string GetInput()
        {
            Console.Write("Type the number of digits: ");
            return Console.ReadLine();
        }

        // returns true if input from user is a positive whole number.
        private static bool IsDigitsOnly(string input)
        {
            foreach (char c in input)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }
    }
}
