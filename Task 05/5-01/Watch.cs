namespace _5_01
{
    using System;
    using System.IO;

    internal class Watch
    {
        private readonly string DbFileName = Program.DbFileName;
        private readonly string DestinationDir = Program.DestinationDir;
        private readonly string SourceDir = Program.SourceDir;
        private readonly string SrcFileTepmlate = Program.SrcFileTepmlate;
        private readonly string DstFileTemplate = Program.DstFileTemplate;
        private readonly char DirSeparator = Program.DirSeparator;

        private IDataSource dataSource;

        public Watch()
        {
            dataSource = new Db(DbFileName);

            if (!Utils.IsDirEmpty(SourceDir, SrcFileTepmlate)
                && Utils.IsDirEmpty(DestinationDir, DstFileTemplate))
            {
                Init();
            }

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.IncludeSubdirectories = true;
            watcher.Path = SourceDir;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Filter = SrcFileTepmlate;
            watcher.Changed += OnChange;
            watcher.Created += OnCreate;
            watcher.Deleted += OnDelete;
            watcher.Renamed += OnRename;

            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Watching for {SourceDir} to {DestinationDir}");
            Console.WriteLine("Enter 'q' to exit.");
            while (Console.Read() != 'q')
            {
            }
        }

        private void OnChange(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            Event curEvent = dataSource.GetLastEventByName(e.FullPath);

            string guid = curEvent?.Guid ?? Guid.NewGuid().ToString();
            int version = (curEvent?.Version ?? 0) + 1;

            this.dataSource.Add(guid, version, e.FullPath, string.Empty, Utils.GetNowInEpoch(), e.ChangeType);
            this.DoBackup(e.FullPath, guid, version);
        }

        private void OnCreate(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            Event curEvent = dataSource.GetLastEventByName(e.FullPath);

            string guid = Guid.NewGuid().ToString();
            int version = 0;

            dataSource.Add(guid, version, e.FullPath, string.Empty, Utils.GetNowInEpoch(), e.ChangeType);
            DoBackup(e.FullPath, guid, version);
        }

        private void OnDelete(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            Event curEvent = dataSource.GetLastEventByName(e.FullPath);

            string guid = curEvent?.Guid ?? Guid.NewGuid().ToString();
            int version = curEvent?.Version ?? 0;

            dataSource.Add(guid, version, e.FullPath, string.Empty, Utils.GetNowInEpoch(), e.ChangeType);
        }

        private void OnRename(object source, RenamedEventArgs e)
        {
            Console.WriteLine("File: " + e.OldFullPath + " renemed to " + e.FullPath);
            Event curEvent = dataSource.GetLastEventByName(e.OldFullPath);

            string guid = curEvent?.Guid ?? Guid.NewGuid().ToString();
            int version = curEvent?.Version ?? 0;

            dataSource.Add(guid, version, e.FullPath, e.OldFullPath, Utils.GetNowInEpoch(), e.ChangeType);
        }

        private void DoBackup(string name, string guid, int version)
        {
            string dest =
                DestinationDir + Path.DirectorySeparatorChar + guid + "." + version.ToString();
            Directory.CreateDirectory(Path.GetDirectoryName(dest));
            File.Copy(name, dest);
        }

        private void Init()
        {
            //int version = 0;
            foreach (var file in Directory.EnumerateFiles(SourceDir, SrcFileTepmlate, SearchOption.AllDirectories))
            {
                FileSystemEventArgs arg = new FileSystemEventArgs(
                    WatcherChangeTypes.Created,
                    SourceDir,
                    file.Replace(SourceDir + DirSeparator, ""));

                OnCreate(new object(), arg);

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
    }
}