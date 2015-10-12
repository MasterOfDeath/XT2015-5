namespace _2_08
{
    using System;

    public class EnemyWolf : Enemy
    {
        private const int DamageLevel = -100;
        private const int SpeedLevel = 4;

        public EnemyWolf()
            : base(DamageLevel, SpeedLevel)
        {
        }

        protected override void DamageAnim()
        {
            throw new NotImplementedException();
        }
    }
}
