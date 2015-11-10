namespace Employees.PL.ConsoleApp
{
    using System;
    using System.Linq;
    using System.Globalization;
    using Employees.BLL.Contract;
    using Employees.Entites;
    using System.Text.RegularExpressions;

    internal class ConsoleUI
    {
        private const string DateFormat = "d.MM.yyyy";

        private static void Main(string[] args)
        {
            IUserLogic userLogic = new BLL.Main.UserMainLogic();
            IAwardLogic awardLogic = new BLL.Main.AwardMainLogic();

            //Regex regName = new Regex(@"[^\w- \.]+");
            //foreach (Match item in regName.Matches(":=Пр2и%веЁт"))
            //{
            //    Console.WriteLine(item.Value);
            //}
            //Console.WriteLine(regName.Matches(":=Пр2ив%еЁт").Count);

            while (true)
            {
                Console.WriteLine("1. Add New Employee\t\t6. Add New Award");
                Console.WriteLine("2. Delete Employee\t\t7. List All Awards");
                Console.WriteLine("3. List All Employees");
                Console.WriteLine("4. Give Award to Employee");
                Console.WriteLine("5. Pull off Award");
                Console.WriteLine();
                Console.WriteLine("0. Exit");
                Console.WriteLine();
                Console.Write("Your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddUser(userLogic);
                        break;

                    case "2":
                        DelUser(userLogic);
                        break;

                    case "3":
                        ListAllUsers(userLogic);
                        break;

                    case "4":
                        RewardUser(userLogic);
                        break;

                    case "5":
                        PullOffAward(userLogic);
                        break;

                    case "6":
                        AddAward(awardLogic);
                        break;

                    case "7":
                        ListAllAwards(awardLogic);
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Incorrect input!\n");
                        break;
                }
            }
        }

        private static void PullOffAward(IUserLogic userLogic)
        {
            try
            {
                Console.Write("Enter Id of Employee: ");
                int userId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Id of Award: ");
                int awardId = Convert.ToInt32(Console.ReadLine());

                if (!userLogic.PullOffAward(userId, awardId))
                {
                    Console.WriteLine("Employee or Award haven't found.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Only integer numbers are possoble.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Out of range");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
        }

        private static void ListAllAwards(IAwardLogic awardLogic)
        {
            try
            {
                foreach (var award in awardLogic.ListAllAwards())
                {
                    Console.WriteLine($"{award.Id}. Title: \"{award.Title}\"");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
        }

        private static void AddAward(IAwardLogic awardlogic)
        {
            try
            {
                Console.Write("Enter Title: ");
                var awardTitle = Console.ReadLine();

                awardlogic.AddAward(awardTitle);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
        }

        private static void RewardUser(IUserLogic userLogic)
        {
            try
            {
                Console.Write("Enter Id of Employee: ");
                int userId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter Id of Award: ");
                int awardId = Convert.ToInt32(Console.ReadLine());

                if (!userLogic.RewardUser(userId, awardId))
                {
                    Console.WriteLine("Employee or Award haven't found.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Only integer numbers are possoble.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Out of range");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
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
                if (!DateTime.TryParseExact(
                    dateStr,
                    DateFormat,
                    CultureInfo.CurrentCulture,
                    DateTimeStyles.None,
                    out date))
                {
                    throw new ArgumentException($"Incorrect input, use this template {DateFormat}");
                }

                var user = new User(name, date);

                logic.AddUser(user);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
        }

        private static void ListAllUsers(IUserLogic logic)
        {
            try
            {
                foreach (var user in logic.ListAll())
                {
                    Console.WriteLine(
                        $"{user.Id}. Name: \"{user.Name}\" " +
                        $"BirthDay: \"{user.BirthDay.ToShortDateString()}\" " +
                        $"Age: \"{user.Age}\"");

                    foreach (var award in user.Awards)
                    {
                        Console.WriteLine($"    Has award: \"{award.Title}\"");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); ;
            }

            Console.WriteLine();
        }

        private static void DelUser(IUserLogic logic)
        {
            try
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
            }
            catch (FormatException)
            {
                Console.WriteLine("Only integer numbers are possoble");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Out of range");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
        }
    }
}
