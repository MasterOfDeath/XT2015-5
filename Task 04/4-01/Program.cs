namespace _4_01
{
    using System;

    internal class Program
    {
        public delegate int CompareMethod<T>(T obj1, T obj2);

        private static void Main(string[] args)
        {
        }

        private static void SortArray<T>(T[] array, CompareMethod<T> compare)
        {
            T buf;
            for (int k = array.Length - 1; k > 0; k--)
            {
                for (int i = 0; i < k; i++)
                {
                    if (compare(array[i], array[i + 1]) > 0)
                    {
                        buf = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = buf;
                    }
                }
            }
        }
    }
}