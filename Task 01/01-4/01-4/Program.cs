//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _01_4
{
    using System;

    /// <summary>
    /// Task 01-4
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main class
        /// </summary>
        /// <param name="args">command line arguments</param>
        public static void Main(string[] args)
        {
            int num = 0;

            Console.Write("Enter number more then 0 and less then 100: ");

            try
            {
                num = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is not a sequence of digits.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("The number cannot fit in an Int32.");
            }

            if (num >= 1 && num <= 100)
            {
                for (int count = 0; count < num; count++)
                {
                    for (var i = 0; i <= count; i++)
                    {
                        for (var j = num - i; j > 0; j--)
                        {
                            Console.Write(" ");
                        }

                        for (var k = -1; k <= i + (i - 1); k++)
                        {
                            Console.Write("*");
                        }

                        Console.WriteLine();
                    }
                }
            }
        }
    }
}