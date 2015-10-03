//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _01_12
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Task 01-12
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main class
        /// </summary>
        /// <param name="args">command line arguments</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter 1st line: ");
            
            // string str1 = Console.ReadLine();
            string str1 = "написать программу, которая";

            Console.WriteLine("Enter 2nd line: ");
            
            // string str2 = Console.ReadLine();
            string str2 = "описание";

            string str3 = MixLines(str1, str2);
            Console.WriteLine(str3);           
        }

        /// <summary>
        /// Mix lines 
        /// </summary>
        /// <param name="str1">String for mixing</param>
        /// <param name="str2">String for compare</param>
        /// <returns>Mixed string</returns>
        private static string MixLines(string str1, string str2)
        {
            HashSet<char> hashSet = new HashSet<char>(str2.ToCharArray());
            StringBuilder result = new StringBuilder();

            foreach (var letter in str1)
            {
                result.Append(letter);

                if (hashSet.Contains(letter))
                {
                    result.Append(letter);
                }
            }
            
            return result.ToString();
        }
    }
}
