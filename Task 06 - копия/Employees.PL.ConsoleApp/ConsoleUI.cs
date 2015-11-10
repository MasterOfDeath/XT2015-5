namespace Employees.PL.ConsoleApp
{
    using System;
    using System.Globalization;
    using Employees.BLL.Contract;
    using Employees.Entites;
    using System.Collections.Generic;

    internal class ConsoleUI
    {
        private static readonly string DateFormat = "d.MM.yyyy";

        private static void Main(string[] args)
        {
            IUserLogic logic = new BLL.Main.UserMainLogic();

            //Console.ReadKey(true);

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
            try
            {
                // Name
                Console.Write("Enter Name: ");
                string name = Console.ReadLine();

                // BirthDay
                DateTime date = new DateTime();
                Console.Write($"Enter BirthDay ({DateFormat}): ");
                string dateStr = Console.ReadLine();
                if(!DateTime.TryParseExact(
                    dateStr,
                    DateFormat,
                    CultureInfo.CurrentCulture,
                    DateTimeStyles.None,
                    out date))
                {
                    throw new ArgumentException($"Incorrect input, use this template {DateFormat}");
                }

                var user = new User(name, date);

                // Awards
                Console.WriteLine("Add award: ");
                var awardStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(awardStr))
                {
                    user.AddAward(new Award(awardStr));
                }

                logic.AddUser(user);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ListAllUsers(IUserLogic logic)
        {
            foreach (var user in logic.ListAll())
            {
                Console.WriteLine(
                    $"{user.Id}. Name: {user.Name} " +
                    $"BirthDay:({user.BirthDay.ToShortDateString()}) " +
                    $"Age: {user.Age}");

                foreach (var award in user.Awards)
                {
                    Console.WriteLine($"\t Has award: \"{award.Title}\"");
                }

                Console.WriteLine();
            }
        }

        private static void DelUser(IUserLogic logic)
        {
            Console.Write("Enter user's number: ");
            int id = Convert.ToInt32(Console.ReadLine());

            if (!logic.DeleteUser(id))
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
