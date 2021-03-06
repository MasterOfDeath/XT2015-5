﻿namespace _4_02
{
    using System;
    using _4_01;

    internal class Program
    {
        private static void Main(string[] args)
        {
            Sort<string> sort = new Sort<string>();

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
            sort.Print(array);

            sort.SortArray(array, CompareStrings);

            Console.WriteLine("After sorting:");
            sort.Print(array);
        }

        private static int CompareStrings(string str1, string str2)
        {
            if (str1.Length != str2.Length)
            {
                return str1.Length - str2.Length;
            }

            return str1.CompareTo(str2);
        }
    }
}