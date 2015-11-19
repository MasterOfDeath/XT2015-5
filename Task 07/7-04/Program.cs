namespace _7_04
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static void Main()
        {
            List<string> strings = new List<string>();
            Console.WriteLine("Введите число (для завершения введите пустую строку): ");

            //var input = "input";
            //while (!string.IsNullOrEmpty(input))
            //{
            //    input = Console.ReadLine();
            //    if (!string.IsNullOrWhiteSpace(input))
            //    {
            //        strings.Add(input);
            //    }
            //}

            strings.Add("40");
            strings.Add("-3.4");
            strings.Add("5.75e-0003");
            strings.Add("57.5e-0003");
            strings.Add("Hello World");

            Console.WriteLine();

            foreach (var str in strings)
            {
                Console.Write("String \"{0}\" is ", str);
                Console.WriteLine(ParseNumbers(str)); 
            }

            Console.WriteLine("Press Enter to exit");
            Console.Read();
        }

        private static string ParseNumbers(string str)
        {
            Regex regCommonNotation = new Regex(@"^[\+-]?(?:[0-9]+|[0-9]+\.[0-9]+)$");

            Regex regExpNotation = new Regex(@"^[\+-]?([1-9](\.[0-9]+)?)e[\+-]?0*[1-9][0-9]*$");

            if (regExpNotation.IsMatch(str))
            {
                return "a number in scientific notation";
            }

            if (regCommonNotation.IsMatch(str))
            {
                return "a number in usual notation";
            }

            return "not a number";
        }
    }
}
