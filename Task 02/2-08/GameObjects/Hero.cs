namespace _2_08
{
    using System;

    public class Hero : GameObject, IMovable
    {
        private const int MaxLife = 1000;
        private int life;
        private int score = 0;

        public Hero(Point position)
        {
            this.Position = position;
            this.life = MaxLife;
        }

        public Point Position { get; set; }

        public int Life
        {
            get
            {
                return this.life;
            }
        }

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
                    this.TakeDamage(Game.EnemyMap[newPosition] as IAttackable);
                    this.DrawDamageAnim();
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

        private void TakeDamage(IAttackable obj)
        {
            this.life += obj.Damage;
        }

        private void TakeBonus(Bonus bonus)
        {
            if (bonus is IScorable)
            {
                this.score += ((IScorable)bonus).Score;
            }

            if (bonus is IHealthy)
            {
                int newLife = this.life + ((IHealthy)bonus).Health;
                this.life = (newLife >= MaxLife) ? MaxLife : newLife;
            }
        }

        private void DrawUpAnim(Point position)
        {
            throw new NotImplementedException();
        }

        private void DrawDownAnim(Point position)
        {
            throw new NotImplementedException();
        }

        private void DrawLeftAnim(Point position)
        {
            throw new NotImplementedException();
        }

        private void DrawRightAnim(Point position)
        {
            throw new NotImplementedException();
        }

        private void DrawDamageAnim()
        {
            throw new NotImplementedException();
        }
    }
}