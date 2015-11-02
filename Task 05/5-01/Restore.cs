namespace _5_01
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    internal class Restore
    {
        private readonly string dataFileName = Program.DataFileName;
        private readonly string destinationDir = Program.DestinationDir;
        private readonly string sourceDir = Program.SourceDir;
        private readonly string srcFileTepmlate = Program.SrcFileTepmlate;

        private IDataSource dataSource;

        public Restore(int epoch)
        {
            this.dataSource = new Db(this.dataFileName);

            IEnumerable<Event> events = this.dataSource.ListToRestore(epoch);

            if (!events.Any())
            {
                return;
            }

            foreach (var item in events)
            {
                Console.WriteLine($"File: {item.Name}; Time: {Utils.Epoch2String(item.Date)}");
            }

            Console.WriteLine("Would you like to restore them? Y/N");
            string answer = Console.ReadLine();
            if (!answer.Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            Utils.CleanDir(this.sourceDir, this.srcFileTepmlate);

            foreach (var item in events.Where(ev => ev.Change != (int)WatcherChangeTypes.Deleted))
            {
                this.DoRestore(item.Name, item.Guid, item.Version);
                Console.WriteLine($"File: {item.Name} has restored.");
            }
        }

        private void DoRestore(string name, string guid, int version)
        {
            string source =
                this.destinationDir + Path.DirectorySeparatorChar + guid + "." + version.ToString();

            Directory.CreateDirectory(Path.GetDirectoryName(name));
            if (Directory.Exists(name))
            {
                this.ReverseDirRename(name + " - copy(1)", name);
            }

            File.Copy(source, name);
        }

        private void ReverseDirRename(string dstName, string sourceName)
        {
            if (!Directory.Exists(dstName))
            {
                Directory.Move(sourceName, dstName);
            }
            else
            {
                dstName = Regex.Replace(
                    dstName, 
                    @"copy\((\d+)\)$",
                    (match) =>
                    {
                        return "copy(" + (Convert.ToInt32(match.Groups[1].Value) + 1).ToString() + ")";
                    });

                this.ReverseDirRename(dstName, sourceName);
            }
        }
    }
}