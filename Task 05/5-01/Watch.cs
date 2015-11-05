namespace _5_01
{
    using System;
    using System.IO;

    internal class Watch
    {
        private readonly string dataFileName = Program.DataFileName;
        private readonly string destinationDir = Program.DestinationDir;
        private readonly string sourceDir = Program.SourceDir;
        private readonly string srcFileTepmlate = Program.SrcFileTepmlate;
        private readonly string dstFileTemplate = Program.DstFileTemplate;
        private readonly char dirSeparator = Program.DirSeparator;

        private IDataSource dataSource;

        public Watch()
        {
            this.dataSource = new Db(this.dataFileName);

            if (!Utils.IsDirEmpty(this.sourceDir, this.srcFileTepmlate)
                && Utils.IsDirEmpty(this.destinationDir, this.dstFileTemplate))
            {
                this.Init();
            }
        }

        public void Start()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.IncludeSubdirectories = true;
            watcher.Path = this.sourceDir;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            watcher.Filter = this.srcFileTepmlate;
            watcher.Changed += this.OnChange;
            watcher.Created += this.OnCreate;
            watcher.Deleted += this.OnDelete;
            watcher.Renamed += this.OnRename;

            watcher.EnableRaisingEvents = true;

            Console.WriteLine($"Watching for {this.sourceDir} to {this.destinationDir}");
            Console.WriteLine("Enter 'q' to exit.");
            while (Console.Read() != 'q')
            {
            }
        }

        private void OnChange(object source, FileSystemEventArgs e)
        {
            if (!Utils.IsDir(e.FullPath))
            {
                Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
                Event curEvent = this.dataSource.GetLastEventByName(e.FullPath);

                string guid;
                int version;

                if (curEvent == null)
                {
                    throw new ArgumentException("Incorrect state of a data store");
                }
                else
                {
                    guid = curEvent.Guid;
                    version = curEvent.Version + 1;
                }

                this.dataSource.Add(guid, version, e.FullPath, string.Empty, Utils.GetNowInEpoch(), e.ChangeType);
                this.DoBackup(e.FullPath, guid, version);
            }
        }

        private void OnCreate(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);

            string guid = Guid.NewGuid().ToString();
            int version = 0;

            this.dataSource.Add(guid, version, e.FullPath, string.Empty, Utils.GetNowInEpoch(), e.ChangeType);
            this.DoBackup(e.FullPath, guid, version);
        }

        private void OnDelete(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            Event curEvent = this.dataSource.GetLastEventByName(e.FullPath);

            string guid;
            int version;

            if (curEvent == null)
            {
                throw new ArgumentException("Incorrect state of a data store");
            }
            else
            {
                guid = curEvent.Guid;
                version = curEvent.Version;
            }

            this.dataSource.Add(guid, version, e.FullPath, string.Empty, Utils.GetNowInEpoch(), e.ChangeType);
        }

        private void OnRename(object source, RenamedEventArgs e)
        {
            Console.WriteLine("File: " + e.OldFullPath + " renemed to " + e.FullPath);
            Event curEvent = this.dataSource.GetLastEventByName(e.OldFullPath);

            string guid;
            int version;

            if (curEvent == null)
            {
                throw new ArgumentException("Incorrect state of a data store");
            }
            else
            {
                guid = curEvent.Guid;
                version = curEvent.Version;
            }

            this.dataSource.Add(guid, version, e.FullPath, e.OldFullPath, Utils.GetNowInEpoch(), e.ChangeType);
        }

        private void DoBackup(string name, string guid, int version)
        {
            string dest =
                this.destinationDir + Path.DirectorySeparatorChar + guid + "." + version.ToString();
            Directory.CreateDirectory(Path.GetDirectoryName(dest));

            FileInfo file = new FileInfo(name);
            file.CopyTo(dest, true);
        }

        private void Init()
        {
            foreach (var file in Directory.EnumerateFiles(this.sourceDir, this.srcFileTepmlate, SearchOption.AllDirectories))
            {
                FileSystemEventArgs arg = new FileSystemEventArgs(
                    WatcherChangeTypes.Created,
                    this.sourceDir,
                    file.Replace(this.sourceDir + this.dirSeparator, ""));

                this.OnCreate(new object(), arg);
            }
        }
    }
}