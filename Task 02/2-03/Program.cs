//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _2_03
{
    using System;

    /// <summary>
    /// Task 2-03
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main class
        /// </summary>
        /// <param name="args">Arguments of command line</param>
        public static void Main(string[] args)
        {
            User user1 = new User("Name1", "Surname1", "MidleName1", DateTime.Parse("01/05/1985"));
            Console.WriteLine(user1.ToString());

            // User user2 = new User("Name1", null, null, DateTime.Parse("26/09/1983"));
        }
    }
}