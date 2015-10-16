namespace _3_01
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    internal class Program
    {
        private static string shift = "    ";
        private static int speed = 100;

        private static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });

            PrintQueue(queue);

            while (queue.Count > 1)
            {
                EnqueueAnimation(queue);
                queue.Enqueue(queue.Dequeue());
                PrintQueue(queue);

                DequeueAnimation(queue);
                queue.Dequeue();
                PrintQueue(queue);
            }

            Console.WriteLine("Last person: {0}", queue.Peek());
            Console.WriteLine();
        }

        private static Position PrintQueue(IEnumerable<int> collection)
        {
            Position curPosition;
            Console.Clear();
            Console.WriteLine();
            Console.Write(shift);
            foreach (var item in collection)
            {
                Console.Write("{0,3}", item);
            }

            curPosition = new Position(Console.CursorLeft, Console.CursorTop);

            Console.WriteLine("\n");

            return curPosition; 
        }

        private static void EnqueueAnimation(Queue<int> queue)
        {
            Position curPos = new Position(shift.Length, 1);

            Thread.Sleep(speed);
            curPos.Left = shift.Length;
            Console.SetCursorPosition(curPos.Left, curPos.Top);
            Console.Write("   ");

            curPos.Top += 2;
           
            for (int i = 0; i < (queue.Count * 3) + 1; i += 2)
            {
                Thread.Sleep(speed);
                
                Console.SetCursorPosition(curPos.Left + i, curPos.Top);
                Console.Write("{0,3}", queue.Peek());

                Console.SetCursorPosition(curPos.Left + i - 2, curPos.Top);
                Console.Write("   ");
            }

            Thread.Sleep(speed);
            
            Console.Write("    ");
            curPos.Left = Console.CursorLeft - 2;
            curPos.Top -= 2;
            Console.SetCursorPosition(curPos.Left, curPos.Top);
            Console.Write(queue.Peek());

            Thread.Sleep(speed);
        }

        private static void DequeueAnimation(Queue<int> queue)
        {
            Position curPos = new Position(shift.Length, 2);
            Console.SetCursorPosition(curPos.Left, curPos.Top);
            Console.Write("   ");

            for (int i = 1; i < shift.Length * 1.5; i += 2)
            {
                Thread.Sleep(speed);

                Console.SetCursorPosition(curPos.Left, curPos.Top + i - 2);
                Console.Write("   ");

                Console.SetCursorPosition(curPos.Left, curPos.Top + i);
                Console.Write("{0,3}", queue.Peek());
            }

            Thread.Sleep(speed);
            
            curPos.Top = Console.CursorTop;
            Console.SetCursorPosition(curPos.Left, curPos.Top);
            Console.Write("  X");

            Thread.Sleep(speed);
            Console.SetCursorPosition(curPos.Left, curPos.Top);
            Console.Write("   ");

            Thread.Sleep(speed);
        }

        private class Position
        {
            public Position(int left, int top)
            {
                this.Left = left;
                this.Top = top;
            }

            public int Left { get; set; }

            public int Top { get; set; }
        }
    }
}