namespace _5_01
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static readonly string DbFileName = "BackupDB.sqlite";
        private static readonly string SourceDir = "c:\\Test";
        private static readonly string DestinationDir = "c:\\Backup";
        private static readonly string FileExtention = "txt";

        private static readonly string Usage = "Usage: --restore dd.MM.yyyy:hh:mm";

        private static Db sqlDb;
        private static FileSystemWatcher watcher;

        private static void Main(string[] args)
        {
            sqlDb = new Db(DbFileName);
            sqlDb.InitEvent += OnInit;

            sqlDb.Init();

            Regex reg = new Regex(
                "^(0[1-9]|[12][0-9]|3[01])" + 
                "\\." +
                "(0[1-9]|1[012])" +
                "\\." +
                "(19\\d{2}|20\\d{2})" +
                "-" +
                "([01][0-9]|2[0-4])" + 
                ":" +
                "([0-5][0-9])$");

            // Console.WriteLine(reg.IsMatch("05.12.2093-00:60"));

            if (args.Length == 0)
            {
                Run();
            }
            else if (args.Length == 2
                && args[0] == "--restore"
                && reg.IsMatch(args[1]))
            {
                Restore();
            }

            

            
        }

        private static void Restore()
        {
            throw new NotImplementedException();
        }

        private static void Run()
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
            return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        private static string Epoch2String(int epoch)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch).ToString();
        }

        private static bool IsDirectory(string str)
            => Path.GetExtension(str) == string.Empty;

        private static void OnInit(object sender, EventArgs e)
        {
            foreach (var file in Directory.EnumerateFiles(SourceDir, "*." + FileExtention, SearchOption.AllDirectories))
            {
                string guid = Guid.NewGuid().ToString();
                int version = 0;

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
            File.Copy(name, dest);
        }

        private bool IsValidDate(string date)
        {
            

            return true;
        }
    }
}