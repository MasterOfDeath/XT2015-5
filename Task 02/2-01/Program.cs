//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _2_01
{
    using System;

    /// <summary>
    /// Task 2-01
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main class
        /// </summary>
        /// <param name="args">arguments of command line</param>
        public static void Main(string[] args)
        {
            Round round = new Round();

            round.R = 10;

            Console.WriteLine("Lenght of circle: {0}", round.CircleLength);
            Console.WriteLine("Area of round: {0}", round.RoundArea);
        }
    }
}
