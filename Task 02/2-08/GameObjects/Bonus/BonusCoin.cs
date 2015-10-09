namespace _2_08
{
    using System;

    public class BonusCoin : Bonus
    {
        private int scoreLevel = 100;

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
