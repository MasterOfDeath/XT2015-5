namespace _7_05
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Введите текст: ");

            var str = Console.ReadLine();
            //var str = "В 7:55 я встал, позавтракал и к 10:77 пошёл на работу.";

            Regex reg = new Regex(@"\b(?:[0-9]|[0-1][0-9]|[2][0-3]):(?:[0-5][0-9])\b");

            Console.WriteLine($"Время в тексте встречается : {reg.Matches(str).Count} раз(а).");

            Console.Read();
        }
    }
}
