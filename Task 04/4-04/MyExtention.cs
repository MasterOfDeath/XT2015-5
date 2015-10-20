namespace ExtensionMethods
{
    using System.Linq;

    public static class MyExtention
    {
        public static int ArraySum(this int[] array)
        {
            return array.Sum(x => x);
        }

        public static bool IsPositiveNumber(this string str)
        {
            foreach (var litter in str)
            {
                if (!char.IsNumber(litter) && litter != '+')
                {
                    return false;
                }
            }

            return true;
        }
    }
}