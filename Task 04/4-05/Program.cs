namespace _4_05
{
    using System;
    using ExtensionMethods;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string[] array = new string[] { "scds", "+1", "34.3", "4332", "-134" };

            foreach (var str in array)
            {
                Console.WriteLine($"Is \'{str}\' a positive integer number? : {str.IsPositiveNumber()}");
            }
        }
    }
}