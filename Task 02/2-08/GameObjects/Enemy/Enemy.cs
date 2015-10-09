namespace _2_08
{
    using System;

    public class Enemy : GameObject
    {
        private Point[] track;

        public Enemy()
        {
            this.Move();
        }

        protected virtual int Speed { get; }

        public virtual void Damage()
        {
        }

        protected virtual void DamageAnim()
        {
        }

        private void Move()
        {
            int length = this.track.Length;
            int step = 0;
            bool direction = true;

            while (true)
            {
                System.Threading.Thread.Sleep(this.Speed * 1000);
                
                for (int i = 0; i < length; i++)
                {
                    if (direction)
                    {
                        step++;
                    }
                    else
                    {
                        step--;
                    }

                    this.Draw(this.track[i]);
                }

                direction = !direction;
            }
        }
    }
}
