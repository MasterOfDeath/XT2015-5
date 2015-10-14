namespace _3_02
{
    using System;
    using System.Collections.Generic;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string str = "tAble glass door glass tool table table";
            string[] strArray = str.Split(new char[] { ' ', '.' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, int> dictionary = new Dictionary<string, int>();

            foreach (var item in strArray)
            {
                if (dictionary.ContainsKey(item.ToLowerInvariant()))
                {
                    dictionary[item] += 1;
                }
                else
                {
                    dictionary.Add(item.ToLowerInvariant(), 1);
                }
            }

            foreach (var item in dictionary)
            {
                Console.WriteLine("{0}\t{1}", item.Key, item.Value);
            }
        }
    }
}