namespace _2_08
{
    using System;

    public class Hero : GameObject
    {
        private GameObject[,] map = Game.Map;
        private string borderClassName = "Border";
        private string bonusClassName = "Bonus";

        public Hero(Point position)
        {
            this.Position = position;
        }

        public Point Position { get; set; }

        public bool HeroMadeCollWithEnemy { get; set; } = false;

        public void MoveUp()
        {
            int x = this.Position.X;
            int newY = this.Position.Y + 1;

            if (newY < Game.Height)
            {
                if (this.Move(x, newY))
                {
                    this.DrawUpAnim(this.Position);
                }
            }
        }

        public void MoveDown()
        {
            int x = this.Position.X;
            int newY = this.Position.Y - 1;

            if (newY > 0)
            {
                if (this.Move(x, newY))
                {
                    this.DrawDownAnim(this.Position);
                }
            }
        }

        public void MoveLeft()
        {
            int newX = this.Position.X - 1;
            int y = this.Position.Y;

            if (newX > 0)
            {
                if (this.Move(newX, y))
                {
                    this.DrawLeftAnim(this.Position);
                }
            }
        }

        public void MoveRight()
        {
            int newX = this.Position.X + 1;
            int y = this.Position.Y;

            if (newX < Game.Width)
            {
                if (this.Move(newX, y))
                {
                    this.DrawRightAnim(this.Position);
                }
            }
        }

        private bool Move(int x, int y)
        {
            if (Game.BorderMap[x, y] != null)
            {
                if (Game.EnemyMap[x, y] != null)
                {
                    this.TakeDamage(Game.EnemyMap[x, y]);
                    return false;
                }

                if (Game.BonusMap[x, y] != null)
                {
                    this.TakeBonus(Game.BonusMap[x, y]);
                }

                this.Position.X = x;

                return true;
            }
            else
            {
                return false;
            }
        }

        private void TakeDamage(Enemy enemy)
        {
            enemy.Damage();
        }

        private void TakeBonus(Bonus bonus)
        {
            bonus.IncScore();
        }

        private void DrawUpAnim(Point position)
        {
        }

        private void DrawDownAnim(Point position)
        {
        }

        private void DrawLeftAnim(Point position)
        {
        }

        private void DrawRightAnim(Point position)
        {
        }
    }
}
