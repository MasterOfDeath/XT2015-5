namespace _2_07
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class Program
    {
        private static List<Shape> shapes = new List<Shape>();

        private static void Main(string[] args)
        {
            while (true)
            {
                ShowMenu();
            }
        }

        private static void ShowMenu()
        {
            Console.Write(
                "-------------------------------------\n" +
                "1) Draw a line.\n" +
                "2) Draw a circle.\n" +
                "3) Draw a ring.\n" +
                "4) Draw a round.\n" +
                "5) Draw a rectangle.\n" +
                "6) Print shapes.\n" +
                "7) Clear all.\n" +
                "8) Exit.\n\n" +
                "Your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    InputLine();
                    break;

                case "2":
                    InpuCircle();
                    break;

                case "3":
                    InputRing();
                    break;

                case "4":
                    InputRound();
                    break;

                case "5":
                    InputRectangle();
                    break;

                case "6":
                    PrintShapes();
                    break;

                case "7":
                    shapes.Clear();
                    break;

                case "8":
                    Environment.Exit(1);
                    break;

                default:
                    Console.WriteLine("Incorrect input!");
                    break;
            }
        }

        private static void PrintShapes()
        {
            foreach (var shape in shapes)
            {
                Console.WriteLine(shape.ToString());
            }
        }

        private static void InputLine()
        {
            int x1 = InputIntegerNumber("Enter X1: ");
            int y1 = InputIntegerNumber("Enter Y1: ");
            var p1 = new Point(x1, y1);
            int x2 = InputIntegerNumber("Enter X2: ");
            int y2 = InputIntegerNumber("Enter Y2: ");
            var p2 = new Point(x2, y2);
            Color color = InputColor("Enter color (default: Black): ");

            shapes.Add(new Line(p1, p2, color));
        }

        private static void InputRectangle()
        {
            int a = InputIntegerNumber("Enter Lenght of A side: ");
            int b = InputIntegerNumber("Enter Lenght of B side: ");
            Color color = InputColor("Enter color (default: Black): ");

            shapes.Add(new Rectangle(a, b, color));
        }

        private static void InputRound()
        {
            int x1 = InputIntegerNumber("Enter X of center: ");
            int y1 = InputIntegerNumber("Enter Y of center: ");
            var center = new Point(x1, y1);
            int r = InputIntegerNumber("Enter radius: ");
            Color color = InputColor("Enter color (default: Black): ");

            shapes.Add(new Round(center, r, color));
        }

        private static void InpuCircle()
        {
            int x1 = InputIntegerNumber("Enter X of center: ");
            int y1 = InputIntegerNumber("Enter Y of center: ");
            var center = new Point(x1, y1);
            int r = InputIntegerNumber("Enter radius: ");
            Color color = InputColor("Enter color (default: Black): ");

            shapes.Add(new Circle(center, r, color));
        }

        private static void InputRing()
        {
            int x1 = InputIntegerNumber("Enter X of center: ");
            int y1 = InputIntegerNumber("Enter Y of center: ");
            var center = new Point(x1, y1);
            int outR = InputIntegerNumber("Enter outer radius: ");
            int inR = InputIntegerNumber("Enter inner radius: ");
            Color color = InputColor("Enter color (default: Black): ");

            shapes.Add(new Ring(center, outR, inR, color));
        }

        private static int InputIntegerNumber(string promptStr)
        {
            int num;

            while (true)
            {
                try
                {
                    Console.Write(promptStr);
                    num = Convert.ToInt32(Console.ReadLine());
                    return num;
                }
                catch (Exception)
                {
                    Console.WriteLine("Input string is not a sequence of digits.");
                }
            }
        }

        private static Color InputColor(string promptStr)
        {
            Color color;

            while (true)
            {
                Console.Write(promptStr);

                string str = Console.ReadLine();

                if (string.IsNullOrEmpty(str))
                {
                    return Color.Black;
                }

                color = Color.FromName(str);

                if ((color.A + color.R + color.G + color.B) != 0)
                {
                    return color;
                }
                else
                {
                    Console.WriteLine("Input string is not a color.");
                }
            }
        }
    }
}