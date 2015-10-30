namespace _5_01
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal interface IDataSource
    {
        event EventHandler<EventArgs> InitEvent;

        void Add(string guid, int version, string fileName, string oldName, int date, WatcherChangeTypes changeType);

        Event GetLastEventByName(string name);

        void Init();

        IEnumerable<Event> ListToRestore(int date);

        IEnumerable<Event> ListAll();
    }
}