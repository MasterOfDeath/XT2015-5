//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _01_10
{
    using System;

    /// <summary>
    /// Task 01-10
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
            int sizeX = 5, sizeY = 4;
            int[,] array = new int[sizeX, sizeY];

            // Fill array 
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    // array[i, j] = random.Next(0, sizeX + sizeY);
                    array[i, j] = i + j;
                }
            }

            // Display current array
            Console.WriteLine("Current array: ");
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    Console.Write("{0},", array[i, j]);
                }

                Console.WriteLine();
            }

            // Sum
            int sumEven = 0;

            /*for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        sumEven += array[i, j];
                    }
                }
            }*/

            for (int i = 0; i < array.Length; i += 2)
            {
                int y = i / sizeX;
                int x = i - (y * sizeX);
                sumEven += array[x, y];
            }

            Console.WriteLine("The Sum of numbers on even positions: {0}", sumEven);
        }
    }
}
