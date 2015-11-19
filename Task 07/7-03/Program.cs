namespace _7_03
{
    using System;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Введите строку: ");

            //var str = Console.ReadLine();
            var str = "rinat.gumirov@mail.ru Петр p_ivanov@mail.rol.org";

            Regex reg = new Regex(
                @"(?:\s|^)([a-zA-Z0-9](?:[\.-_]?[a-zA-Z0-9])*@[a-zA-Z0-9](?:-?[a-zA-Z0-9])*(?:\.[a-zA-Z0-9](-?[a-zA-Z0-9])*)*\.[a-zA-z]{2,6})(?:\s|$)");

            foreach (Match item in reg.Matches(str))
            {
                Console.WriteLine(item.Groups[1].Value);
            }

            Console.WriteLine("\nPress Enter to Exit.");
            Console.Read();
        }
    }
}
