namespace _5_01
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    internal class Program
    {
        private static readonly string FileTepmlate = "*.txt";
        private static readonly string DateFormat = "dd.MM.yyyy-HH:mm";
        private static readonly string AssemblyName = Assembly.GetCallingAssembly().GetName().Name;
        private static readonly char DirSeparator = Path.DirectorySeparatorChar;
        private static readonly string DbFileName = ConfigurationManager.AppSettings["DbFileName"];
        private static readonly string DestinationDir = ConfigurationManager.AppSettings["DestinationDir"].TrimEnd(DirSeparator);
        private static readonly string SourceDir = ConfigurationManager.AppSettings["SourceDir"].TrimEnd(DirSeparator);
        private static readonly string OptRestore = "--restore";
        private static readonly string OptListAll = "--listall";
        private static readonly string OptReInit = "--reinit";
        private static readonly string Usage =
            "Usage:\n" +
            "For watching:\n" +
            $"\tJust launch it without arguments and it'll start for watching {SourceDir}\n" +
            "For restore files:\n" +
            $"\t{AssemblyName} {OptRestore} {DateFormat}\n" +
            $"For print all log:\n\t{AssemblyName} {OptListAll}\n" +
            $"For delete all backups use:\n\t{AssemblyName} {OptReInit}";

        private static IDataSource dataSource;
        private static FileSystemWatcher watcher;

        private static void Main(string[] args)
        {
            int epoch;

            if (args.Length == 1 && args[0] == OptReInit)
            {
                ReInit();
            }

            dataSource = new Db(DbFileName);

            //File.Copy(@"c:\source\3.txt\новый текстовый документ.txt", @"c:\source\3.txt", true);
            
            if (args.Length == 0)
            {
                Watch();
            }
            else if (args.Length == 1
                && args[0] == OptListAll)
            {
                ListAll();
            }
            else if (args.Length == 2
                && args[0] == OptRestore
                && String2Epoch(args[1], out epoch))
            {
                Restore(epoch);
            }
            else
            {
                Console.WriteLine(Usage);
            }
        }

        private static void ReInit()
        {
            Console.WriteLine("Are you sure? Y/N");
            string answer = Console.ReadLine();
            if (answer.Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                File.Delete(DbFileName);
                CleanDir(DestinationDir, "????????-????-????-????-????????????.*");

                Environment.Exit(0);
            }
        }

        private static void Restore(int epoch)
        {
            IEnumerable<Event> events = dataSource.ListToRestore(epoch);

            if (!events.Any())
            {
                return;
            }

            foreach (var item in events)
            {
                Console.WriteLine($"File: {item.Name}; Time: {Epoch2String(item.Date)}");
            }

            Console.WriteLine("Would you like to restore them? Y/N");
            string answer = Console.ReadLine();
            if (!answer.Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            CleanDir(SourceDir, FileTepmlate);

            foreach (var item in events.Where(ev => ev.Change != (int)WatcherChangeTypes.Deleted))
            {
                DoRestore(item.Name, item.Guid, item.Version);
                Console.WriteLine($"File: {item.Name} has restored.");
            }
        }

        private static void ListAll()
        {
            foreach (var item in dataSource.ListAll())
            {
                Console.WriteLine(
                    $"File: {item.Name}; Change: {Enum.GetName(typeof(WatcherChangeTypes), item.Change)}; Time: {Epoch2String(item.Date)}");
            }
        }

        private static void Watch()
        {
            if (!IsDirEmpty(SourceDir, FileTepmlate)
                && IsDirEmpty(DestinationDir, "????????-????-????-????-????????????.*"))
            {
                Init();
            }

            watcher = new FileSystemWatcher();
            watcher.IncludeSubdirectories = true;
            watcher.Path = SourceDir;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Filter = FileTepmlate;
            watcher.Changed += new FileSystemEventHandler(OnChange);
            watcher.Created += new FileSystemEventHandler(OnChange);
            watcher.Deleted += new FileSystemEventHandler(OnChange);
            watcher.Renamed += new RenamedEventHandler(OnRename);

            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Watching for {SourceDir} to {DestinationDir}");
            Console.WriteLine("Enter 'q' to exit.");
            while (Console.Read() != 'q')
            {
            }
        }

        private static void OnChange(object source, FileSystemEventArgs e)
        {
            string guid;
            int version;
            
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            Event curEvent = dataSource.GetLastEventByName(e.FullPath);

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
                    version = (curEvent?.Version ?? 0) + 1;
                    break;

                default:
                    goto case WatcherChangeTypes.Created;
            }

            dataSource.Add(guid, version, e.FullPath, string.Empty, GetNowInEpoch(), e.ChangeType);

            if (e.ChangeType != WatcherChangeTypes.Deleted)
            {
                DoBackup(e.FullPath, guid, version);
            }
        }

        private static void OnRename(object source, RenamedEventArgs e)
        {
            Event curEvent;
            string guid;
            int version;

            Console.WriteLine("File: " + e.OldFullPath + " renemed to " + e.FullPath);

            curEvent = dataSource.GetLastEventByName(e.OldFullPath);

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

            dataSource.Add(guid, version, e.FullPath, e.OldFullPath, GetNowInEpoch(), e.ChangeType);
        }

        private static int GetNowInEpoch()
        {
            return (int)(DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        private static string Epoch2String(int epoch)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(epoch).ToString();
        }

        private static bool String2Epoch(string strDate, out int epoch)
        {
            bool result;

            DateTime date = DateTime.Now;
            result = DateTime.TryParseExact(
                strDate,
                DateFormat,
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out date);
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            epoch = (int)(date - origin).TotalSeconds;

            return result;
        }

        private static bool IsDirectory(string str)
        {
            return Path.GetExtension(str) == string.Empty;
        }

        private static void Init()
        {
            int version = 0;
            foreach (var file in Directory.EnumerateFiles(SourceDir, FileTepmlate, SearchOption.AllDirectories))
            {
                FileSystemEventArgs arg = new FileSystemEventArgs(
                    WatcherChangeTypes.Created,
                    SourceDir,
                    file.Replace(SourceDir + DirSeparator, ""));

                OnChange(new object(), arg);

                //string guid = Guid.NewGuid().ToString();

                //dataSource.Add(
                //    guid,
                //    version,
                //    file,
                //    string.Empty,
                //    GetNowInEpoch(),
                //    WatcherChangeTypes.Created);

                //DoBackup(file, guid, version);
            }
        }

        private static void DoBackup(string name, string guid, int version)
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

        private static void CleanDir(string dirName, string fileTepmlate)
        {
            foreach (var file in Directory.EnumerateFiles(dirName, fileTepmlate, SearchOption.AllDirectories))
            {
                File.Delete(file);
            }
        }

        private static bool IsDirEmpty(string dirName, string fileTepmlate)
        {
            foreach (var file in Directory.EnumerateFiles(dirName, fileTepmlate, SearchOption.AllDirectories))
            {
                return false;
            }

            return true;
        }
    }
}