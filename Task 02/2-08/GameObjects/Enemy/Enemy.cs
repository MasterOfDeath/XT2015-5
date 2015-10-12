namespace _2_08
{
    using System;

    public class Enemy : GameObject, IMovable, IAttackable
    {
        private Point[] track;

        public Enemy(int damage, int speed)
        {
            this.Move();
            this.Damage = damage;
            this.Speed = speed;
        }

        public int Damage { get; }

        public int Speed { get; }

        protected virtual void DamageAnim()
        {
            throw new NotImplementedException();
        }

        private void Move()
        {
            throw new NotImplementedException();
        }
    }
}
