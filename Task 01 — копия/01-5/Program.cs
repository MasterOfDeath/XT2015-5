//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="MyCompany" author="Rinat Gumirov">
//     Be free to use it.
// </copyright>
//-----------------------------------------------------------------------

namespace _01_5
{
    using System;

    /// <summary>
    /// Task 01-5
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main class
        /// </summary>
        /// <param name="args">command line arguments</param>
        public static void Main(string[] args)
        {
            int sum3 = 0,
            sum5 = 0;

            // Находим сумму чисел от 1 до 1000 кратных 3 
            for (int i = 0; i <= 1000; i += 3)
            {
                sum3 = sum3 + i;
            }

            Console.WriteLine("Сумма чисел от 1 до 1000 кратных 3: {0}", sum3);

            // Находим сумму чисел от 1 до 1000 кратных 5 
            for (int i = 0; i <= 1000; i += 5)
            {
                // Проверяем чтоб, при этом не кратно 3-м
                if ((i % 3) != 0)  
                {
                    sum5 = sum5 + i;
                }
            }

            Console.WriteLine("Сумма чисел от 1 до 1000 кратных 5: {0}", sum5);
            Console.WriteLine("Итоговая сумма: {0}", sum3 + sum5);
        }
    }
}
