namespace _2_08
{
    using System;

    public class EnemyWolf : Enemy
    {
        private int damageLevel = 100;

        protected override int Speed { get; } = 10;

        public override void Damage()
        {
            Game.Life -= this.damageLevel;
            this.DamageAnim();
        }

        protected override void DamageAnim()
        {
        }
    }
}
