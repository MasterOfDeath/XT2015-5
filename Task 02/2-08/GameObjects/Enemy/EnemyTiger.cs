namespace _2_08
{
    using System;

    public class EnemyTiger : Enemy
    {
        private int damageLevel = 200;

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
