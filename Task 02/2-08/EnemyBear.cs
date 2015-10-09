namespace _2_08
{
    using System;

    public class EnemyBear : Enemy
    {
        private int damageLevel = 400;

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
