namespace _2_08
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Game.LoadMapFromResource("level01.map");

            if (Game.BonusMap[2, 5] == null)
            {
                Console.WriteLine("Null");
            }
            else
            {
                Console.WriteLine("Not null");
            }

            Console.WriteLine(Game.Map[2, 5].GetType().Name.ToString());
        }
    }
}
