//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _01_5
{
    using System;

    /// <summary>
    /// Task 01-5
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main class
        /// </summary>
        /// <param name="args">command line arguments</param>
        public static void Main(string[] args)
        {
            int range = 1000;
            int sum3 = Prog(3, range);
            int sum5 = Prog(5, range);
            int sum15 = Prog(15, range);

            int sum = sum3 + sum5 - sum15;

            Console.WriteLine("The total sum: {0}", sum);
        }

        /// <summary>
        /// Arithmetical progression S(n) = (a1 + an) * n / 2
        /// </summary>
        /// <param name="a1">Start of range</param>
        /// <param name="range">End of range</param>
        /// <returns>Sum of range</returns>
        private static int Prog(int a1, int range)
        {
            int n = range / a1;
            int max = n * a1;

            return (a1 + max) * n / 2;
        }
    }
}
