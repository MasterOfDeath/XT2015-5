namespace _2_08
{
    using System;

    public class BonusHeart : BonusHealth
    {
        private const int HealthLevel = 1000;

        public BonusHeart()
            : base(HealthLevel)
        {
        }

        public override void AnimateBonus()
        {
            throw new NotImplementedException();
        }
    }
}
