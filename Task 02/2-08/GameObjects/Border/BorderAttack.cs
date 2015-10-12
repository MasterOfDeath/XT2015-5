namespace _2_08
{
    public class BorderAttack : Border, IAttackable
    {
        public BorderAttack(int damage)
        {
            this.Damage = damage;
        }

        public int Damage { get; }
    }
}