namespace _2_05
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var employee1 = new Employee("Ринат", "Гумиров", "Ильгизович", DateTime.Parse("01/05/1985"), 4, DateTime.Parse("06/12/2017"));
            Console.WriteLine(employee1.ToString());
        }
    }
}