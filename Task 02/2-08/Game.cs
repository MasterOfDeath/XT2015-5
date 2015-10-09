namespace _2_08
{
    using System;

    public class Game
    {
        private static GameObject[,] map;

        private static Bonus[,] bonusMap;

        private static Border[,] borderMap;

        private static Enemy[,] enemyMap; 

        public static int Width { get; } = 600;

        public static int Height { get; } = 600;

        public static int Life { get; set; } = 1000;

        public static int Score { get; set; } = 0;

        public static GameObject[,] Map
        {
            get { return map; }
        }

        public static Bonus[,] BonusMap
        {
            get { return bonusMap; }
        }

        public static Border[,] BorderMap
        {
            get { return borderMap; }
        }

        public static Enemy[,] EnemyMap
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
            map = new GameObject[Width, Height];

            bonusMap = new Bonus[Width, Height];
            borderMap = new Border[Width, Height];
            enemyMap = new Enemy[Width, Height];

            map[2, 5] = new Border();
            
            return true;
        }
    }
}
