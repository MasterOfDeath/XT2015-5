//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _01_9
{
    using System;

    /// <summary>
    /// Task 01-9
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main class
        /// </summary>
        /// <param name="args">command line arguments</param>
        public static void Main(string[] args)
        {
            Random random = new Random();
            const int Size = 10;
            int[] array = new int[Size];

            // Fill array 
            for (int i = 0; i < Size; i++)
            {
                array[i] = random.Next(-Size, Size);
            }

            // Display current array
            Console.Write("Current array: ");
            foreach (var i in array)
            {
                Console.Write("{0},", i);
            }

            Console.WriteLine();

            // Count
            int sumPos = 0;
            foreach (var item in array)
            {
                if (item > 0)
                {
                    sumPos += item;
                }
            }

            Console.WriteLine("The Sum of non-negative numbers: {0}", sumPos);
        }
    }
}
