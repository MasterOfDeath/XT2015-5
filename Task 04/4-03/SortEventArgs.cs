namespace _4_03
{
    using System;

    public class SortEventArgs : EventArgs
    {
        public SortEventArgs(int id)
        {
            this.ID = id;
        }

        public int ID { get; }
    }
}