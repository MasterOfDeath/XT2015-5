namespace _2_08
{
    using System;

    public interface IStr
    {
        int GetDamage { get; }
    }

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

            GameObject[] rt = new GameObject[2];
            try
            {
                rt[0] = new EnemyBear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.TargetSite);
            }
        }
    }
}