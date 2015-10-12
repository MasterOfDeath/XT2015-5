namespace _2_08
{
    using System;

    public class EnemyTiger : Enemy
    {
        private const int DamageLevel = -200;
        private const int SpeedLevel = 3;

        public EnemyTiger()
            : base(DamageLevel, SpeedLevel)
        {
        }

        protected override void DamageAnim()
        {
            throw new NotImplementedException();
        }
    }
}
