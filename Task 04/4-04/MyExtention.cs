namespace ExtensionMethods
{
    using System.Linq;

    public static class MyExtention
    {
        public static int? ArraySum(this byte?[] array)
        {
            return array.Sum(x => x);
        }

        public static int ArraySum(this byte[] array)
        {
            return array.Sum(x => x);
        }

        public static int? ArraySum(this sbyte?[] array)
        {
            return array.Sum(x => x);
        }

        public static int ArraySum(this sbyte[] array)
        {
            return array.Sum(x => x);
        }

        public static decimal? ArraySum(this decimal?[] array)
        {
            return array.Sum(x => x);
        }

        public static decimal ArraySum(this decimal[] array)
        {
            return array.Sum(x => x);
        }

        public static double? ArraySum(this double?[] array)
        {
            return array.Sum(x => x);
        }

        public static double ArraySum(this double[] array)
        {
            return array.Sum(x => x);
        }

        public static float? ArraySum(this float?[] array)
        {
            return array.Sum(x => x);
        }

        public static float ArraySum(this float[] array)
        {
            return array.Sum(x => x);
        }

        public static long? ArraySum(this long?[] array)
        {
            return array.Sum(x => x);
        }

        public static long ArraySum(this long[] array)
        {
            return array.Sum(x => x);
        }

        public static int? ArraySum(this int?[] array)
        {
            return array.Sum(x => x);
        }

        public static int ArraySum(this int[] array)
        {
            return array.Sum(x => x);
        }
    }
}