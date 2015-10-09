namespace _2_08
{
    using System;

    public class BonusApple : Bonus
    {
        private int scoreLevel = 10;

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
