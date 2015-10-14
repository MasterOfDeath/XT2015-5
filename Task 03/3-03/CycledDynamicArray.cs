namespace _3_03
{
    using System;
    using System.Collections.Generic;

    class CycledDynamicArray<T> : DynamicArray<T>
    {

        public new IEnumerator<T> GetEnumerator()
        {
            bool direction = true;
            int n = -1;

            for (int i = 0; i < this.Length; i++)
            {
                if (direction)
                {
                    n++;
                }
                else
                {
                    n--;
                }

                yield return this.array[n];
            }

            direction = !direction;
        }
    }
}
