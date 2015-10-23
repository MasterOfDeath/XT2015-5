namespace _4_05
{
    using System;
    using ExtensionMethods;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] array = new string[] 
            {
                "scds",
                "+1",
                "34.3",
                "4332",
                "-134",
                "12.3e1",
                "2356E+5",
                "-67e+12",
                "0.45e-7",
                "5.678e+3",
            };

            foreach (var str in array)
            {
                Console.WriteLine($"Is \'{str}\' a positive integer number? : {str.IsPositiveNumber()}");
            }
        }
    }
}