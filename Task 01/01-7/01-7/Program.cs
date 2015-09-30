//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _01_7
{
    using System;

    /// <summary>
    /// Task 01-7
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
                array[i] = random.Next(0, Size);
            }

            // Display current array
            Console.Write("Current array: ");
            foreach (var i in array)
            {
                Console.Write("{0},", i);
            }

            Console.WriteLine();

            // Looking for Min. and Max.
            int minVal = array[0], maxVal = array[0];
            for (int i = 1; i < Size; i++)
            {
                if (array[i] < minVal)
                {
                    minVal = array[i];
                }

                if (array[i] > maxVal)
                {
                    maxVal = array[i];
                }
            }

            Console.WriteLine("Min. value: {0}, Max. value: {1}", minVal, maxVal);

            // Sort our array
            int buf;
            for (int k = Size - 1; k > 0; k--)
            {
                for (int i = 0; i < k; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        buf = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = buf;
                    }
                }
            }        

            // Display sorted array
            Console.Write("Sorted array: ");
            foreach (var i in array)
            {
                Console.Write("{0},", i);
            }

            Console.WriteLine();
        }
    }
}
