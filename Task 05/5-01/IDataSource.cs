namespace _5_01
{
    using System.Collections.Generic;
    using System.IO;

    internal interface IDataSource
    {
        void Add(string guid, int version, string fileName, string oldName, int date, WatcherChangeTypes changeType);

        Event GetLastEventByName(string name);

        IEnumerable<Event> ListToRestore(int date);

        IEnumerable<Event> ListAll();
    }
}