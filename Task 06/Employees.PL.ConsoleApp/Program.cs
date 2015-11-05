namespace Employees.PL.ConsoleApp
{
    using System;
    using System.Globalization;
    using Employees.BLL.Contract;
    using Employees.BLL.Main;
    using Employees.Entites;

    internal class Program
    {
        private static readonly string DateFormat = "d.MM.yyyy";

        private static void Main(string[] args)
        {
            var logic = new UserMainLogicCreator().CreateInstance();

            while (true)
            {
                Console.WriteLine("1. Add user");
                Console.WriteLine("2. Delete user");
                Console.WriteLine("3. List all users");
                Console.WriteLine("0. Exit");

                Console.Write("Your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddUser(logic);
                        break;

                    case "2":
                        DelUser(logic);
                        break;

                    case "3":
                        ListAllUsers(logic);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Incorrect input!\n");
                        break;
                }
            }
        }

        private static void AddUser(IUserLogic logic)
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            DateTime date = new DateTime();
            bool incorrect = true;
            while (incorrect)
            {
                Console.Write($"Enter BirthDay ({DateFormat}): ");
                string dateStr = Console.ReadLine();

                incorrect = !DateTime.TryParseExact(
                    dateStr,
                    DateFormat,
                    CultureInfo.CurrentCulture,
                    DateTimeStyles.None,
                    out date);

                if (incorrect)
                {
                    Console.WriteLine($"Incorrect input, use this template {DateFormat}");
                }
            }

            logic.Add(new User(name, date));
        }

        private static void ListAllUsers(IUserLogic logic)
        {
            foreach (var user in logic.ListAll())
            {
                Console.WriteLine(
                    $"{user.Id}. Name: {user.Name} " +
                    $"BirthDay:({user.BirthDay.ToShortDateString()}) " +
                    $"Age: {user.Age}");
            }

            Console.WriteLine();
        }

        private static void DelUser(IUserLogic logic)
        {
            Console.Write("Enter user's number: ");
            int id = Convert.ToInt32(Console.ReadLine());

            if (!logic.Delete(id))
            {
                Console.WriteLine($"The user {id} hasn't found.");
            }
            else
            {
                Console.WriteLine($"The user {id} has been deleted succusefully.");
            }

            Console.WriteLine();
        }
    }
}
