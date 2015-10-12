namespace _2_08
{
    using System;

    public class BonusCoin : Bonus, IScorable
    {
        private int scoreLevel = 100;

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
