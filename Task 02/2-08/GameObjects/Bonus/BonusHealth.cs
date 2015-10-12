namespace _2_08
{
    public class BonusHealth : Bonus, IHealthy
    {
        public BonusHealth(int health)
        {
            this.Health = health;
        }

        public int Health { get; }
    }
}
