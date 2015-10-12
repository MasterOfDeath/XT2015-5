namespace _2_08
{
    using System;

    public class BonusApple : Bonus, IScorable
    {
        private int scoreLevel = 10;

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