namespace _4_06
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Util
    {
        public static List<int> OnlyPositive(int[] array)
        {
            List<int> result = new List<int>(array.Length + 10);

            foreach (var item in array)
            {
                if (item > 0)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static List<int> OnlyPositive(int[] array, DelPredicate isPositive)
        {
            List<int> result = new List<int>(array.Length + 10);

            foreach (var item in array)
            {
                if (isPositive(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static List<int> OnlyPositiveAnonim(int[] array, Func<int, bool> isPositive)
        {
            List<int> result = new List<int>(array.Length + 10);

            foreach (var item in array)
            {
                if (isPositive(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public static List<int> OnlyPositiveLinq(int[] array)
        {
            return array
                .Where(x => (x > 0))
                .ToList();
        }
    }
}