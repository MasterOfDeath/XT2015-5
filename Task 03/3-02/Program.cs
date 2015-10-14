namespace _3_02
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string str = "table glass door glass tool table table";
            string[] strArray = str.Split(new char[]{ ' ', '.'}, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> dic = new Dictionary<string, int>();

            foreach (var item in strArray)
            {
                if (dic.ContainsKey(item))
                {
                    dic[item] += 1;
                }
                else
                {
                    dic.Add(item, 1);
                }
            }

            foreach (var item in dic)
            {
                Console.WriteLine("{0}\t{1}", item.Key, item.Value);
            }
        }
    }
}