namespace _2_08
{
    using System;

    public class BonusDiamond : Bonus, IScorable
    {
        private int scoreLevel = 1000;

        public int Score
        {
            get
            {
                return this.scoreLevel;
            }
        }

        public override void AnimateBonus()
        {
            throw new NotImplementedException();
        }
    }
}
