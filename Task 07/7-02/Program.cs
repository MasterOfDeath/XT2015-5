namespace _7_02
{
    using System;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Введите текст: ");

            // var str = Console.ReadLine();
            var str = "<b>Это</b> <br/> текст <i>с</i> <font color=\"red\">HTML</font> кодами";

            // Regex reg = new Regex(@"<[/]?.+?>"); 
            // Regex reg = new Regex(@"(?></?\w+)(?>(?:[^>'""]+|'[^']*'|""[^""]*"")*)>");
            Regex reg = new Regex(@"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>");

            Console.WriteLine(reg.Replace(str, "_"));

            Console.Read();
        }
    }
}
