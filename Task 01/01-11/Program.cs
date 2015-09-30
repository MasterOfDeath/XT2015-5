//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _01_11
{
    using System;

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
            char[] symbols = { ' ', '=', '"', '\t', '!', '?', ':', ';', ',', '.' };
            Console.WriteLine("Enter your sentence: ");
            string sentence = "copyright file = \"Program\" company = \"MyCompany\" author = \"Rinat Gumirov\"";

            string[] res = sentence.Split(symbols);

            int count = 0, sum = 0;
            foreach (var item in res)
            {
                if (item != string.Empty)
                {
                    count++;
                    sum += item.Length;
                    Console.WriteLine("Word \"{0}\" has {1} litters", item, item.Length);
                } 
            }

            Console.WriteLine("Average: {0}", (float)sum / count);
        }
    }
}
