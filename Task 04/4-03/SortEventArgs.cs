namespace _4_03
{
    using System;

    internal class SortEventArgs : EventArgs
    {
        public SortEventArgs(int id)
        {
            this.ID = id;
        }

        public int ID { get; set; }
    }
}