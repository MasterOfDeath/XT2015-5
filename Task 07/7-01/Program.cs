namespace _7_01
{
    using System;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static void Main()
        {
            Console.Write("Введите текст, содержащий дату в формате dd-mm-yyyy: ");
            
            // var str = Console.ReadLine();
            var str = "2016 год наступит 01-02-2016";
            Console.WriteLine();

            var datePattern = @"(\D|^)((0[1-9]|[12][0-9])-(0[1-9]|1[0-2])|30-(0[13-9]|1[0-2])|31-(0[13578]|1[02]))-\d{4}(\D|$)";

            Regex reg = new Regex(datePattern);

            if (reg.IsMatch(str))
            {
                Console.WriteLine("В тексте \"{0}\" содержится дата.", str);
            }
            else
            {
                Console.WriteLine("Тексте \"{0}\" не содержит дату.", str);
            }

            Console.WriteLine();
            Console.Read();
        }
    }
}
