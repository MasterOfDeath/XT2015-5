namespace ExtensionMethods
{
    using System.Linq;

    public static class MyExtention
    {
        public static int ArraySum(this byte[] array)
        {
            return array.Sum(x => x);
        }

        public static int ArraySum(this sbyte[] array)
        {
            return array.Sum(x => x);
        }

        public static int ArraySum(this short[] array)
        {
            return array.Sum(x => x);
        }

        public static int ArraySum(this ushort[] array)
        {
            return array.Sum(x => x);
        }

        public static int ArraySum(this int[] array)
        {
            return array.Sum(x => x);
        }

        public static long ArraySum(this uint[] array)
        {
            return array.Sum(x => x);
        }

        public static long ArraySum(this long[] array)
        {
            return array.Sum(x => x);
        }

        public static ulong ArraySum(this ulong[] array)
        {
            ulong result = 0;
            foreach (var item in array)
            {
                result += item;
            }

            return result;
        }

        public static float ArraySum(this float[] array)
        {
            return array.Sum(x => x);
        }

        public static double ArraySum(this double[] array)
        {
            return array.Sum(x => x);
        }

        public static decimal ArraySum(this decimal[] array)
        {
            return array.Sum(x => x);
        }
    }
}