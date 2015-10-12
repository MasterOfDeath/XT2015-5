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
            throw new NotImplementedException();
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
