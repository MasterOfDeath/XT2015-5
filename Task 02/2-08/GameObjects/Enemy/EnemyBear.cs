namespace _2_08
{
    using System;

    public class EnemyBear : Enemy
    {
        private const int DamageLevel = -400;
        private const int SpeedLevel = 10;

        public EnemyBear()
            : base(DamageLevel, SpeedLevel)
        {
        }

        protected override void DamageAnim()
        {
            throw new NotImplementedException();
        }
    }
}