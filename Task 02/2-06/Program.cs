namespace _2_06
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var ring1 = new Ring(new Point(2, 3), 12, 4);
            Console.WriteLine(ring1.ToString());
        }
    }
}