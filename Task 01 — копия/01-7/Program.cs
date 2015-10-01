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
            const int Size = 10;
            int[] array = new int[Size];

            FillArray(array, 0, array.Length);

            // Display current array
            Console.Write("Current array: ");
            DisplayArray(array);

            // Looking for Min. and Max.
            Console.WriteLine("Min. value: {0}, Max. value: {1}", MinValue(array), MaxValue(array));

            // Sort our array
            SortArray(array);        

            // Display sorted array
            Console.Write("Sorted array: ");
            DisplayArray(array);
        }

        /// <summary>
        /// Fill array by random values
        /// </summary>
        /// <param name="array">Array for filling</param>
        /// <param name="startRange">Start range</param>
        /// <param name="stopRange">Stop range</param>
        private static void FillArray(int[] array, int startRange, int stopRange)
        {
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(startRange, stopRange);
            }
        }

        /// <summary>
        /// Display Array
        /// </summary>
        /// <param name="array">Array for filling</param>
        private static void DisplayArray(int[] array)
        {
            foreach (var i in array)
            {
                Console.Write("{0},", i);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Looking for min value of array
        /// </summary>
        /// <param name="array">Array for research</param>
        /// <returns>The min value</returns>
        private static int MinValue(int[] array)
        {
            int minVal = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < minVal)
                {
                    minVal = array[i];
                }
            }

            return minVal;
        }

        /// <summary>
        /// Looking for max value of array
        /// </summary>
        /// <param name="array">Array for research</param>
        /// <returns>The max value</returns>
        private static int MaxValue(int[] array)
        {
            int maxVal = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > maxVal)
                {
                    maxVal = array[i];
                }
            }

            return maxVal;
        }

        /// <summary>
        /// Sort Array
        /// </summary>
        /// <param name="array">Array for sorting</param>
        private static void SortArray(int[] array)
        {
            int buf;
            for (int k = array.Length - 1; k > 0; k--)
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
        }
    }
}
