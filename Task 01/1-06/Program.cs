﻿//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _01_6
{
    using System;

    /// <summary>
    /// Task 01-6
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Text options
        /// </summary>
        [Flags]
        private enum TextOpt : byte
        {
            /// <summary>
            /// None opt
            /// </summary>
            None = 0,

            /// <summary>
            /// Bold text
            /// </summary>
            bold = 1,

            /// <summary>
            /// Italic text
            /// </summary>
            italic = 2,

            /// <summary>
            /// Underline text
            /// </summary>
            underline = 4,
        }

        /// <summary>
        /// Main class
        /// </summary>
        /// <param name="args">command line parameters</param>
        public static void Main(string[] args)
        {
            string choice;
            TextOpt textOpt = TextOpt.None;

            while (true)
            {
                Console.WriteLine("Text options: {0}", textOpt);
                Console.WriteLine("Enter:\n\t 1: bold\n\t 2: italic\n\t 3: underline");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        textOpt ^= TextOpt.bold;
                        break;

                    case "2":
                        textOpt ^= TextOpt.italic;
                        break;

                    case "3":
                        textOpt ^= TextOpt.underline;
                        break;

                    default:
                        Console.WriteLine("Invalid input!");
                        break;
                }
            }                        
        }
    }
}
