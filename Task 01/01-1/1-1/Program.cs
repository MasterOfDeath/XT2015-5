//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------
namespace _1_1
{
    using System;

    /// <summary>
    /// Task 01-1
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main class
        /// </summary>
        /// <param name="args">command line parameters</param>
        public static void Main(string[] args)
        {
            int length = 0, width = 0, square = 0;

            length = SetValue(0);
            width = SetValue(1);
            square = length * width;

            Console.WriteLine("Length = {0}, Width = {1}, Square = {2}", length, width, square);
        }

        /// <summary>
        /// Validate input
        /// </summary>
        /// <param name="side">0 = length, 1 = width</param>
        /// <returns>validated value</returns>
        private static int SetValue(int side)
        {
            string input;
            int result = 0;
            bool stay = true;

            while (stay)
            {
                try
                {
                    if (side == 0)
                    {
                        Console.Write("Enter a length: ");
                    }
                    else
                    {
                        Console.Write("Enter a width: ");
                    }

                    input = Console.ReadLine();
                    result = Convert.ToInt32(input);

                    if (result <= 0)
                    {
                        Console.WriteLine("The value mustn't be negative or 0");
                        stay = true;
                    }
                    else
                    {
                        stay = false;
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Input string is not a sequence of digits.");
                    stay = true;
                }
                catch (OverflowException e)
                {
                    Console.WriteLine("The number cannot fit in an Int32.");
                    stay = true;
                }
            }

            return result;
        }
    }
}
