﻿namespace _4_04
{
    using System;
    using ExtensionMethods;

    internal class Program
    {
        private static void Main(string[] args)
        {
            byte[] array = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Console.WriteLine(array.ArraySum());
        }
    }
}