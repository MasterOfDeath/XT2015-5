namespace _4_03
{
    using System;
    using System.Threading;
    using _4_01;

    internal class ThreadedSort : Sort
    {
        public event EventHandler<EventArgs> Finish;
        
        public void SortArrayInThread<T>(T[] array, Func<T, T, int> compare, int threadID)
        {
            new Thread(() =>
            {
                this.SortArray(array, compare);
                this.Finish?.Invoke(this, new SortEventArgs(threadID));
            }).Start();
        }
    }
}