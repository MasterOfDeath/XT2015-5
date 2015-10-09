namespace _2_08
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Game.LoadMapFromResource("level01.map");

            Point point = new Point(2, 5);

            if (Game.BonusMap.ContainsKey(point))
            {
                Console.WriteLine("Not null");
            }
            else
            {
                Console.WriteLine("Null");
            }
        }
    }
}
