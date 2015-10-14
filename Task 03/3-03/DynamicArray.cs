namespace _3_03
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DynamicArray<T> : IEnumerable<T>, IEnumerable, ICloneable
    {
        private const int DefaultCapacity = 8;

        protected T[] array;

        public DynamicArray()
            : this(DefaultCapacity)
        {
        }

        public DynamicArray(int n)
        {
            this.array = this.Generate(n);
            this.Length = 0;
        }

        public DynamicArray(DynamicArray<T> toClone)
        {
            this.array = this.Generate(toClone.array.Length);
            toClone.array.CopyTo(this.array, 0);
            this.Length = toClone.Length;
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            int count = 0;
            int n = 0;
            IEnumerator<T> enumerator = collection.GetEnumerator();

            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                count++;
            }

            this.array = this.Generate(count + DefaultCapacity);

            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                this.array[n] = enumerator.Current;
                n++;
            }

            this.Length = count;
        }

        public int Capacity
        {
            get
            {
                return this.array.Length;
            }

            set
            {
                if (value > 0)
                {
                    this.SetCapacity(value);
                }
                else
                {
                    throw new ArgumentException("Capacity mustn't be negative or 0!");
                }
            }
        }

        public int Length { get; private set; }

        public T this[int id]
        {
            get
            {
                if (id >= -this.Length && id < this.Length)
                {
                    if (id < 0)
                    {
                        id = this.Length + id;
                    }

                    return this.array[id];
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            set
            {
                if (id >= -this.Length && id < this.Length)
                {
                    if (id < 0)
                    {
                        id = this.Length + id;
                    }

                    this.array[id] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Add(T newItem)
        {
            if (this.Length >= this.Capacity - 1)
            {
                this.Capacity *= 2;
            }

            this.array[this.Length] = newItem;
            this.Length++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            int count = 0;
            int n = 0;
            IEnumerator<T> enumerator = collection.GetEnumerator();

            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                count++;
            }

            T[] newArray = this.Generate(count);

            enumerator.Reset();
            while (enumerator.MoveNext())
            {
                newArray[n] = enumerator.Current;
                n++;
            }

            if (this.Length + count >= this.Capacity - 1)
            {
                this.Capacity = this.Length + count + DefaultCapacity;
            }

            Array.Copy(newArray, 0, this.array, this.Length, count);

            this.Length += count;
        }

        public bool Remove(T item)
        {
            int id = Array.IndexOf(this.array, item);
            if (id >= 0)
            {
                T[] newArray = new T[this.Capacity];
                Array.Copy(this.array, newArray, id);
                Array.Copy(this.array, id + 1, newArray, id, this.Length - id - 1);
                this.array = newArray;
                this.Length--;
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool Insert(int id, T item)
        {
            if (id >= 0 && id < this.Length)
            {
                if (this.Length >= this.Capacity - 1)
                {
                    this.Capacity *= 2;
                }

                T[] newArray = new T[this.Capacity];
                Array.Copy(this.array, newArray, id);
                Array.Copy(this.array, id, newArray, id + 1, this.Length - id);
                newArray[id] = item;
                this.array = newArray;
                this.Length++;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }

            return true;
        }

        public T[] ToArray()
        {
            T[] newArray = Generate(this.Length);
            Array.Copy(this.array, newArray, this.Length);

            return newArray;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
            {
                yield return this.array[i];
            }
        }

        public object Clone()
        {
            //return this.MemberwiseClone();
            return new DynamicArray<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private T[] Generate(int n)
        {
            T[] newArray = new T[n];

            // for (int i = 0; i < newArray.Length; i++)
            // {
            //    newArray[i] = default(T);
            // }
            return newArray;
        }

        private void SetCapacity(int newCapacity)
        {
            T[] newArray = this.Generate(newCapacity);

            int newLength = (newCapacity < this.Length) ? newCapacity : this.Length;

            Array.Copy(this.array, newArray, newLength);
            this.array = newArray;
            this.Length = newLength;
        }
    }
}