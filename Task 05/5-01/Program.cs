namespace _5_01
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;

    internal class Program
    {
        private static readonly string DbFileName = "BackupDB.sqlite";
        private static readonly string SourceDir = "c:\\Test";
        private static readonly string DestinationDir = "c:\\Backup";
        private static readonly string FileExtention = "txt";
        private static readonly string DateFormat = "dd.MM.yyyy-HH:mm";

        private static readonly string Usage = $"Usage:\n For restore files: --restore {DateFormat}" +
            "\n For watching: just launch it without arguments.";

        private static Db sqlDb;
        private static FileSystemWatcher watcher;

        private static void Main(string[] args)
        {
            sqlDb = new Db(DbFileName);
            sqlDb.InitEvent += OnInit;

            sqlDb.Init();

            if (args.Length == 0)
            {
                Watch();
            }
            else if (args.Length == 2
                && args[0] == "--restore"
                && IsValidDate(args[1]))
            {
                Restore(args[1]);
            }
            else
            {
                Console.WriteLine(Usage);
            }
        }

        private static void Restore(string date)
        {
            IEnumerable<Event> events = sqlDb.ListToRestore(String2Epoch(date));

            if (events.Any())
            {
                CleanSourceDir();

                foreach (var item in events)
                {
                    DoRestore(item.Name, item.Guid, item.Version);
                    Console.WriteLine($"File: {item.Name} has restored.");
                }
            }
        }

        private static void Watch()
        {
            watcher = new FileSystemWatcher();
            watcher.IncludeSubdirectories = true;
            watcher.Path = SourceDir;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Filter = "*." + FileExtention;
            watcher.Changed += new FileSystemEventHandler(OnChange);
            watcher.Created += new FileSystemEventHandler(OnChange);
            watcher.Deleted += new FileSystemEventHandler(OnChange);
            watcher.Renamed += new RenamedEventHandler(OnRename);

            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Enter 'q' to exit.");
            while (Console.Read() != 'q')
            {
            }
        }

        private static void OnChange(object source, FileSystemEventArgs e)
        {
            Event curEvent;
            string guid;
            int version;

            try
            {
                watcher.EnableRaisingEvents = false;

                Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
                curEvent = sqlDb.GetLastEventByName(e.FullPath);

                switch (e.ChangeType)
                {
                    case WatcherChangeTypes.Created:
                        guid = Guid.NewGuid().ToString();
                        version = 0;
                        break;

                    case WatcherChangeTypes.Deleted:
                        guid = curEvent?.Guid ?? Guid.NewGuid().ToString();
                        version = curEvent?.Version ?? 0;
                        break;

                    case WatcherChangeTypes.Changed:
                        guid = curEvent?.Guid ?? Guid.NewGuid().ToString();
                        version = (curEvent != null) ? curEvent.Version + 1 : 0;
                        break;

                    default:
                        goto case WatcherChangeTypes.Created;
                }

                sqlDb.Add(guid, version, e.FullPath, string.Empty, GetNowInEpoch(), e.ChangeType);
                DoBuckup(e.FullPath, guid, version);
            }
            finally
            {
                watcher.EnableRaisingEvents = true;
            }
        }

        private static void OnRename(object source, RenamedEventArgs e)
        {
            Event curEvent;
            string guid;
            int version;

            Console.WriteLine("File: " + e.OldFullPath + " renemed to " + e.FullPath);

            curEvent = sqlDb.GetLastEventByName(e.OldFullPath);

            if (curEvent == null)
            {
                guid = Guid.NewGuid().ToString();
                version = 0;
            }
            else
            {
                guid = curEvent.Guid;
                version = curEvent.Version;
            }

            sqlDb.Add(guid, version, e.FullPath, e.OldFullPath, GetNowInEpoch(), e.ChangeType);
        }

        private static int GetNowInEpoch()
        {
            return (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private static string Epoch2String(int epoch)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch).ToString();
        }

        static int String2Epoch(string strDate)
        {
            DateTime date = DateTime.Now;
            DateTime.TryParseExact(
                strDate, 
                DateFormat, 
                CultureInfo.CurrentCulture, 
                DateTimeStyles.None, 
                out date);
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (int)(date - origin).TotalSeconds;
        }

        private static bool IsDirectory(string str)
        {
            return Path.GetExtension(str) == string.Empty;
        }

        private static void OnInit(object sender, EventArgs e)
        {
            int version = 0;
            foreach (var file in Directory.EnumerateFiles(SourceDir, "*." + FileExtention, SearchOption.AllDirectories))
            {
                string guid = Guid.NewGuid().ToString();
                
                sqlDb.Add(
                    guid,
                    version,
                    file,
                    string.Empty,
                    GetNowInEpoch(),
                    WatcherChangeTypes.Created);

                DoBuckup(file, guid, version);
            }
        }

        private static void DoBuckup(string name, string guid, int version)
        {
            string dest =
                DestinationDir + Path.DirectorySeparatorChar + guid + "." + version.ToString();
            Directory.CreateDirectory(Path.GetDirectoryName(dest));
            File.Copy(name, dest);
        }

        private static void DoRestore(string name, string guid, int version)
        {
            string dest =
                DestinationDir + Path.DirectorySeparatorChar + guid + "." + version.ToString();
            Directory.CreateDirectory(Path.GetDirectoryName(name));
            File.Copy(dest, name);
        }

        private static void CleanSourceDir()
        {
            foreach (var file in Directory.EnumerateFiles(SourceDir, "*." + FileExtention, SearchOption.AllDirectories))
            {
                File.Delete(file);
            }
        }

        private static bool IsValidDate(string date)
        {
            Regex reg = new Regex(
                "^(0[1-9]|[12][0-9]|3[01])" +
                "\\." +
                "(0[1-9]|1[012])" +
                "\\." +
                "(19\\d{2}|20\\d{2})" +
                "-" +
                "([01][0-9]|2[0-3])" +
                ":" +
                "([0-5][0-9])$");

            return reg.IsMatch(date);
        }
    }
}