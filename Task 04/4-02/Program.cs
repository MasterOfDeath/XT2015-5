namespace _4_02
{
    using System;

    internal class Program
    {
        public delegate int CompareMethod<T>(T obj1, T obj2);

        private static void Main(string[] args)
        {
            Random random = new Random();
            string[] array = new string[10] 
            {
                "table",
                "coffee",
                "tea",
                "juice",
                "1",
                "apple",
                "sea",
                "tree",
                "leopard",
                "ant",
            };

            Console.WriteLine("Before sorting:");
            Print(array);

            SortArray(array, new CompareMethod<string>(CompareStrings));

            Console.WriteLine("After sorting:");
            Print(array);
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

        private static int CompareStrings(string str1, string str2)
        {
            // <
            if (str1.Length < str2.Length)
            {
                return -1;
            }

            // >
            if (str1.Length > str2.Length)
            {
                return 1;
            }

            // ==
            return str1.CompareTo(str2);
        }

        private static int CompareByHash<T>(T obj1, T obj2)
        {
            if (obj1.GetHashCode() < obj2.GetHashCode())
            {
                return -1;
            }

            if (obj1.GetHashCode() > obj2.GetHashCode())
            {
                return 1;
            }

            return 0;
        }

        private static void Print<T>(T[] array)
        {
            foreach (var item in array)
            {
                Console.Write("{0} ", item);
            }

            Console.WriteLine("\n");
        }
    }
}