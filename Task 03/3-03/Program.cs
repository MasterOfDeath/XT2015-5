namespace _3_03
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            DynamicArray<int> array1 = new DynamicArray<int>(new int[] { 1, 2, 3, 4, 5 });
            array1.AddRange(new int[] { 7, 5, 8, 10, 65 });

            Print(array1);

            array1.Add(555);

            Print(array1);

            array1[-2] = 34;
            Print(array1);

            array1.Capacity = 4;
            array1.Remove(2);
            Print(array1);

            array1.Add(1);
            array1.Add(2);
            array1.Add(3);
            array1.Add(4);
            Print(array1);

            DynamicArray<int> array2 = (DynamicArray<int>)array1.Clone();
            array2.Capacity = 2;
            Print(array1);
            Print(array2);
        }

        private static void Print(DynamicArray<int> array)
        {
            foreach (var item in array)
            {
                Console.Write("{0} ", item);
            }

            Console.WriteLine("Capacity: {0}, Length: {1}", array.Capacity, array.Length);
        }
    }
}