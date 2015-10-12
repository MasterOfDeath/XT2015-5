namespace _2_08
{
    using System;

    public class BorderAttack : Border, IAttackable
    {
        public BorderAttack(int damage)
        {
            this.Damage = damage;
        }

        public int Damage { get; }
    }
}
