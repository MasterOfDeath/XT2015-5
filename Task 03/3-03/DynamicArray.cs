namespace _3_03
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DynamicArray<T> : IEnumerable<T>, IEnumerable, ICloneable
    {
        private const int DefaultCapacity = 8;

        private T[] array;

        public DynamicArray()
            : this(DefaultCapacity)
        {
        }

        public DynamicArray(int n)
        {
            this.array = new T[n];
            this.Length = 0;
        }

        public DynamicArray(DynamicArray<T> toClone)
        {
            this.array = new T[toClone.array.Length];
            toClone.array.CopyTo(this.array, 0);
            this.Length = toClone.Length;
        }

        public DynamicArray(IEnumerable<T> collection)
        {
            T[] newArray = collection.ToArray<T>();

            this.array = new T[newArray.Length + DefaultCapacity];
            Array.Copy(newArray, this.array, newArray.Length);

            this.Length = newArray.Length;
        }

        public int Capacity
        {
            get
            {
                return this.array.Length;
            }
        }

        public int Length { get; private set; }

        public T this[int id]
        {
            get
            {
                if (id < -this.Length && id >= this.Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    if (id < 0)
                    {
                        id = this.Length + id;
                    }

                    return this.array[id];
                }
            }

            set
            {
                if (id < -this.Length && id >= this.Length)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    if (id < 0)
                    {
                        id = this.Length + id;
                    }

                    this.array[id] = value;
                }
            }
        }

        public void Add(T newItem)
        {
            if (this.Length >= this.Capacity - 1)
            {
                this.SetCapacity(this.Capacity * 2);
            }

            this.array[this.Length] = newItem;
            this.Length++;
        }

        public void AddRange(IEnumerable<T> collection)
        {
            T[] newArray = collection.ToArray<T>();

            if (this.Length + newArray.Length >= this.Capacity - 1)
            {
                this.SetCapacity(this.Length + newArray.Length + DefaultCapacity);
            }

            Array.Copy(newArray, 0, this.array, this.Length, newArray.Length);

            this.Length += newArray.Length;
        }

        public object Clone()
        {
            return new DynamicArray<T>(this);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
            {
                yield return this.array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool Insert(int id, T item)
        {
            if (id < 0 && id >= this.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                if (this.Length >= this.Capacity - 1)
                {
                    this.SetCapacity(this.Capacity * 2);
                }

                T[] newArray = new T[this.Capacity];
                Array.Copy(this.array, newArray, id);
                Array.Copy(this.array, id, newArray, id + 1, this.Length - id);
                newArray[id] = item;
                this.array = newArray;
                this.Length++;
            }

            return true;
        }

        public bool Remove(T item)
        {
            int id = Array.IndexOf(this.array, item);

            if (id < 0)
            {
                return false;
            }
            else
            {
                T[] newArray = new T[this.Capacity];
                Array.Copy(this.array, newArray, id);
                Array.Copy(this.array, id + 1, newArray, id, this.Length - id - 1);
                this.array = newArray;
                this.Length--;
            }

            return true;
        }

        public T[] ToArray()
        {
            T[] newArray = new T[this.Length];
            Array.Copy(this.array, newArray, this.Length);

            return newArray;
        }

        public void SetCapacity(int newCapacity)
        {
            if (newCapacity <= 0)
            {
                throw new ArgumentException("Capacity mustn't be negative or 0!");
            }
            else
            {
                T[] newArray = new T[newCapacity];

                int newLength = (newCapacity < this.Length) ? newCapacity : this.Length;

                Array.Copy(this.array, newArray, newLength);
                this.array = newArray;
                this.Length = newLength;
            }
        }
    }
}