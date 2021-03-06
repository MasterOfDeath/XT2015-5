﻿namespace _4_01
{
    using System;

    public class Sort<T>
    {
        public void SortArray(T[] array, Func<T, T, int> compare)
        {
            if (compare == null)
            {
                throw new ArgumentNullException(nameof(compare));
            }

            for (int k = array.Length - 1; k > 0; k--)
            {
                for (int i = 0; i < k; i++)
                {
                    if (compare(array[i], array[i + 1]) > 0)
                    {
                        T buf = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = buf;
                    }
                }
            }
        }

        public void Print(T[] array)
        {
            foreach (var item in array)
            {
                Console.Write("{0} ", item);
            }

            Console.WriteLine("\n");
        }
    }
}
