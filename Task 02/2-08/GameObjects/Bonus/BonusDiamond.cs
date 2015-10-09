namespace _2_08
{
    using System;

    public class BonusDiamond : Bonus
    {
        private int scoreLevel = 1000;

        public override void IncScore()
        {
            Game.Score += this.scoreLevel;
            this.AnimateBonus();
        }

        public override void AnimateBonus()
        {
        }
    }
}
