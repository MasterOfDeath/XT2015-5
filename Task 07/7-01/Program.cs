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
            var str = "2016 год наступит 01-01-2016";
            Console.WriteLine();

            // Regex reg = new Regex(@"(\D|^)([0][1-9]|[12][0-9]|[3][01])-([0][1-9]|[1][0-2])-(\d{4})(\D|$)");
            Regex reg = new Regex(@"\b([0][1-9]|[12][0-9]|[3][01])-([0][1-9]|[1][0-2])-(\d{4})\b");

            if (reg.IsMatch(str))
            {
                Console.WriteLine("В тексте \"{0}\" содержится дата.", str);
            }
            else
            {
                Console.WriteLine("Тексте \"{0}\" не содержит дату.", str);
            }

            Console.Read();
        }
    }
}
