namespace _3_03
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class CycledDynamicArray<T> : DynamicArray<T>, IEnumerable<T>, IEnumerable, ICloneable
    {
        private const int DefaultCapacity = 8;

        public CycledDynamicArray()
            : this(DefaultCapacity)
        {
        }

        public CycledDynamicArray(int n)
            : base(n)
        {
        }

        public CycledDynamicArray(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public CycledDynamicArray(DynamicArray<T> toClone)
            : base(toClone)
        {
        }

        public new IEnumerator<T> GetEnumerator()
        {
            /*IEnumerator<T> enumerator = base.GetEnumerator();

            while (true)
            {
                if (enumerator.MoveNext())
                {
                    yield return enumerator.Current;
                }
                else
                {
                    enumerator = base.GetEnumerator();
                }
            }*/

            while (true)
            {
                foreach (var item in (DynamicArray<T>)this)
                {
                    yield return item;
                }
            }
        }
    }
}