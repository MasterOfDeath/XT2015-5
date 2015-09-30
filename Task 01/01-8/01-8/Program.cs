//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _01_8
{
    using System;

    /// <summary>
    /// Task 01-8
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
            int sizeX = 4, sizeY = 2, sizeZ = 3;
            int[,,] array = new int[sizeX, sizeY, sizeZ];

            // Fill array
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    for (int k = 0; k < sizeZ; k++)
                    {
                        array[i, j, k] = random.Next(-5,5);
                    }
                }
            }

            // Transform array
            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    for (int k = 0; k < sizeZ; k++)
                    {
                        if (array[i, j, k] > 0)
                        {
                            array[i, j, k] = 0;
                        }
                    }
                }
            }
        }
    }
}
