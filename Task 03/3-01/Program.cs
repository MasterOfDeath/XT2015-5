namespace _3_01
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] array = new int[10];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }

            Queue<int> crowd = new Queue<int>(array);

            while (crowd.Count > 1)
            {
                crowd.Enqueue(crowd.Dequeue());
                crowd.Dequeue();
            }

            Console.WriteLine("Last element: {0}", crowd.Peek());
        }
    }
}