namespace _5_01
{
    internal class Event
    {
        public static readonly string TableName = "history";
        public static readonly string FId = "id";
        public static readonly string FGuid = "guid";
        public static readonly string FVersion = "version";
        public static readonly string FName = "name";
        public static readonly string FOldName = "old_name";
        public static readonly string FDate = "date";
        public static readonly string FChange = "change";

        public Event(int id, string guid, int version, string name, string oldName, int date, int change)
        {
            this.ID = id;
            this.Guid = guid;
            this.Version = version;
            this.Name = name;
            this.OldName = oldName;
            this.Date = date;
            this.Change = change;
        }

        public int ID { get; set; }

        public string Guid { get; set; }

        public int Version { get; set; }

        public string Name { get; set; }

        public string OldName { get; set; }

        public int Date { get; set; }

        public int Change { get; set; }
    }
}