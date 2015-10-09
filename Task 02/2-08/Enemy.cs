namespace _2_08
{
    using System;

    public class Enemy : GameObject
    {
        private Point[] track;

        protected virtual int Speed { get; }

        public virtual void Damage()
        {
        }

        protected virtual void DamageAnim()
        {
        }

        private void DoStep()
        {

        }
    }
}
