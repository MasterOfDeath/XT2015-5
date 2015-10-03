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
            int n3 = 1000 / 3;
            int max3 = n3 * 3;
            int sum3 = Prog(3, max3, n3);

            int n5 = 1000 / 5;
            int max5 = n5 * 5;
            int sum5 = Prog(5, max5, n5);

            int n15 = 1000 / 15;
            int max15 = n15 * 15;
            int sum15 = Prog(15, max15, n15);

            int sum = sum3 + sum5 - sum15;

            Console.WriteLine("The total sum: {0}", sum);
        }

        /// <summary>
        /// Arithmetical progression S(n) = (a1 + an) * n / 2
        /// </summary>
        /// <param name="a1">Start of range</param>
        /// <param name="max">End of range</param>
        /// <param name="n">Amount of numbers in range</param>
        /// <returns>Sum of range</returns>
        private static int Prog(int a1, int max, int n)
            => (a1 + max) * n / 2;
    }
}
