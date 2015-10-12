//-----------------------------------------------------------------------
// <copyright file="program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _2_02
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
        /// <param name="args">Arguments of command line</param>
        public static void Main(string[] args)
        {
            var triangle = new Triangle(8, 15, 17);

            Console.WriteLine("Perimeter: {0}", triangle.Perimeter);
            Console.WriteLine("Area: {0}", triangle.Area);
        }
    }
}