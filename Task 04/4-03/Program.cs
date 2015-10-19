namespace _4_03
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            int[] array1 = new int[] { 2, 1, 4, 3, 5 };
            int[] array2 = new int[] { 10, 9, 6, 8, 7 };

            ThreadedSort su = new ThreadedSort();
            su.Finish += (sender, eventArgs) 
                => Console.WriteLine($"Thread #{((SortEventArgs)eventArgs).ID} has been finished.");             

            su.SortArrayInThread(array1, CompareInt, 1);
            su.SortArrayInThread(array2, CompareInt, 2);
        }

        private static int CompareInt(int obj1, int obj2)
        {
            return obj1 - obj2;
        }
    }
}