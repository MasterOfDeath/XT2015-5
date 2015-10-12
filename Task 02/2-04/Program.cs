//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _2_04
{
    using System;

    /// <summary>
    /// Task 2-04
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main class
        /// </summary>
        /// <param name="args">Command line arguments</param>
        private static void Main(string[] args)
        {
            var myStr1 = new MyString("Privet!");
            Console.WriteLine(myStr1);

            myStr1[4] = 'a';
            Console.WriteLine(myStr1);

            var myStr2 = new MyString("Kak Dela?");
            Console.WriteLine(myStr1 + myStr2);

            string str1 = myStr2;
            Console.WriteLine(str1);

            char[] str2 = (char[])myStr1;
            Console.WriteLine(str2);

            MyString myStr3 = (MyString)str1;
            MyString myStr4 = (MyString)str2;
        }
    }
}