//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _01_11
{
    using System;
    using System.Text;

    /// <summary>
    /// Task 01-11
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main class
        /// </summary>
        /// <param name="args">command line arguments</param>
        public static void Main(string[] args)
        {           
            Console.Write("Enter your sentence: ");
            string sentence = "copyright file = \"Program\" company = \"MyCompany\" author = \"Rinat Gumirov\"";
            Console.WriteLine(sentence);
            char[] symbols = GetNonLitSymbols(sentence);

            string[] res = sentence.Split(symbols, StringSplitOptions.RemoveEmptyEntries);

            int sum = 0;
            foreach (var item in res)
            {
                sum += item.Length;
                Console.WriteLine("Word \"{0}\" has {1} litters", item, item.Length);
            }

            Console.WriteLine("Average: {0}", (float)sum / res.Length);
        }

        /// <summary>
        /// Get all non literal symbols in given sentence
        /// </summary>
        /// <param name="str">Given sentence</param>
        /// <returns>All non literal symbols in given sentence</returns>
        private static char[] GetNonLitSymbols(string str)
        {
            StringBuilder result = new StringBuilder();

            foreach (char symbol in str)
            {
                if (!char.IsLetter(symbol))
                {
                    result.Append(symbol);
                }
            }

            return result.ToString().ToCharArray();
        }
    }
}
