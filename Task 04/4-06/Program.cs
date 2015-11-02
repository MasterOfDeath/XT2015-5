namespace _4_06
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    internal delegate bool DelPredicate(int x);

    internal class Program
    {
        private static void Main(string[] args)
        {
            int arraySize = 5000;
            int testsCount = 100;
            Random random = new Random();

            int[] array = new int[arraySize];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(-100, 100);
            }

            // Uses the second Core or Processor for the Test
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(2); 

            // Prevents "Normal" processes from interrupting Threads
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;

            // Prevents "Normal" Threads from interrupting this thread
            Thread.CurrentThread.Priority = ThreadPriority.Highest;     

            Stopwatch stopWatch = new Stopwatch();
            List<long> results = new List<long>(testsCount + 10);

            // --- 1.
            for (int i = 0; i < testsCount; i++)
            {
                stopWatch.Start();
                Util.OnlyPositive(array);
                stopWatch.Stop();

                results.Add(stopWatch.ElapsedTicks);
                stopWatch.Reset();
            }

            results.Clear();

            for (int i = 0; i < testsCount; i++)
            {
                stopWatch.Start();
                Util.OnlyPositive(array);
                stopWatch.Stop();

                results.Add(stopWatch.ElapsedTicks);
                stopWatch.Reset();
            }

            results.Sort();
            Console.WriteLine("Simple method: \t\t{0}", results[results.Count / 2]);
            results.Clear();

            // --- 2.
            DelPredicate del = new DelPredicate(IsPositive);

            for (int i = 0; i < testsCount; i++)
            {
                stopWatch.Start();
                Util.OnlyPositive(array, del);
                stopWatch.Stop();

                results.Add(stopWatch.ElapsedTicks);
                stopWatch.Reset();
            }

            results.Clear();

            for (int i = 0; i < testsCount; i++)
            {
                stopWatch.Start();
                Util.OnlyPositive(array, del);
                stopWatch.Stop();

                results.Add(stopWatch.ElapsedTicks);
                stopWatch.Reset();
            }

            results.Sort();
            Console.WriteLine("Simple delegate: \t{0}", results[results.Count / 2]);
            results.Clear();

            // --- 3.
            for (int i = 0; i < testsCount; i++)
            {
                stopWatch.Start();
                Util.OnlyPositive(array, new DelPredicate(x => (x > 0)));
                stopWatch.Stop();

                results.Add(stopWatch.ElapsedTicks);
                stopWatch.Reset();
            }

            results.Clear();

            for (int i = 0; i < testsCount; i++)
            {
                stopWatch.Start();
                Util.OnlyPositive(array, new DelPredicate(x => (x > 0)));
                stopWatch.Stop();

                results.Add(stopWatch.ElapsedTicks);
                stopWatch.Reset();
            }

            results.Sort();
            Console.WriteLine("Lambda method: \t\t{0}", results[results.Count / 2]);
            results.Clear();

            // --- 4.
            for (int i = 0; i < testsCount; i++)
            {
                stopWatch.Start();
                Util.OnlyPositiveAnonim(array, x => (x > 0));
                stopWatch.Stop();

                results.Add(stopWatch.ElapsedTicks);
                stopWatch.Reset();
            }

            results.Clear();

            for (int i = 0; i < testsCount; i++)
            {
                stopWatch.Start();
                Util.OnlyPositiveAnonim(array, x => (x > 0));
                stopWatch.Stop();

                results.Add(stopWatch.ElapsedTicks);
                stopWatch.Reset();
            }

            results.Sort();
            Console.WriteLine("Anonim method: \t\t{0}", results[results.Count / 2]);
            results.Clear();

            // --- 5.
            for (int i = 0; i < testsCount; i++)
            {
                stopWatch.Start();
                Util.OnlyPositiveLinq(array);
                stopWatch.Stop();

                results.Add(stopWatch.ElapsedTicks);
                stopWatch.Reset();
            }

            results.Clear();

            for (int i = 0; i < testsCount; i++)
            {
                stopWatch.Start();
                Util.OnlyPositiveLinq(array);
                stopWatch.Stop();

                results.Add(stopWatch.ElapsedTicks);
                stopWatch.Reset();
            }

            results.Sort();
            Console.WriteLine("Linq method: \t\t{0}", results[results.Count / 2]);
            results.Clear();
        }

        private static bool IsPositive(int x)
        {
            return x > 0;
        }
    }
}