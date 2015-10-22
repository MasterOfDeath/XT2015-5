namespace _4_03
{
    using System;
    using System.Threading;
    using _4_01;

    public class ThreadedSort<T> : Sort<T>
    {
        public event EventHandler<SortEventArgs> Finish;
        
        public void SortArrayInThread(T[] array, Func<T, T, int> compare, int threadID)
        {
            new Thread(() =>
            {
                this.SortArray(array, compare);
                this.Finish?.Invoke(this, new SortEventArgs(threadID));
            }).Start();
        }
    }
}