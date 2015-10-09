namespace _2_08
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        private static Dictionary<Point, Bonus> bonusMap;

        private static Dictionary<Point, Border> borderMap;

        private static Dictionary<Point, Enemy> enemyMap;

        public static int Width { get; } = 600;

        public static int Height { get; } = 600;

        public static int Life { get; set; } = 1000;

        public static int Score { get; set; } = 0;

        public static Dictionary<Point, Bonus> BonusMap
        {
            get { return bonusMap; }
        }

        public static Dictionary<Point, Border> BorderMap
        {
            get { return borderMap; }
        }

        public static Dictionary<Point, Enemy> EnemyMap
        {
            get { return enemyMap; }
        }

        public static void GameOver()
        {
        }

        public static void MakeDamage(int damage)
        {
            Life -= damage;

            if (Life <= 0)
            {
                GameOver();
            }
        }

        public static bool LoadMapFromResource(string res)
        {
            bonusMap = new Dictionary<Point, Bonus>();
            borderMap = new Dictionary<Point, Border>();
            enemyMap = new Dictionary<Point, Enemy>();

            return true;
        }
    }
}
