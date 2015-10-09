namespace _2_08
{
    using System;

    public class Hero : GameObject
    {
        public Hero(Point position)
        {
            this.Position = position;
        }

        public Point Position { get; set; }

        public bool HeroMadeCollWithEnemy { get; set; } = false;

        public void MoveUp()
        {
            Point newPosition = new Point(this.Position.X, this.Position.Y + 1);

            if (newPosition.Y < Game.Height)
            {
                if (this.Move(newPosition))
                {
                    this.DrawUpAnim(this.Position);
                }
            }
        }

        public void MoveDown()
        {
            Point newPosition = new Point(this.Position.X, this.Position.Y - 1);

            if (newPosition.Y > 0)
            {
                if (this.Move(newPosition))
                {
                    this.DrawDownAnim(this.Position);
                }
            }
        }

        public void MoveLeft()
        {
            Point newPosition = new Point(this.Position.X - 1, this.Position.Y);

            if (newPosition.X > 0)
            {
                if (this.Move(newPosition))
                {
                    this.DrawLeftAnim(this.Position);
                }
            }
        }

        public void MoveRight()
        {
            Point newPosition = new Point(this.Position.X + 1, this.Position.Y);

            if (newPosition.X < Game.Width)
            {
                if (this.Move(newPosition))
                {
                    this.DrawRightAnim(this.Position);
                }
            }
        }

        private bool Move(Point newPosition)
        {
            if (!Game.BorderMap.ContainsKey(newPosition))
            {
                if (Game.EnemyMap.ContainsKey(newPosition))
                {
                    this.TakeDamage(Game.EnemyMap[newPosition]);
                    return false;
                }

                if (Game.BonusMap.ContainsKey(newPosition))
                {
                    this.TakeBonus(Game.BonusMap[newPosition]);
                }

                this.Position = newPosition;

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
