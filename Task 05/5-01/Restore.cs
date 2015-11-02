using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _5_01
{
    internal class Restore
    {
        private readonly string DbFileName = Program.DbFileName;
        private readonly string DestinationDir = Program.DestinationDir;
        private readonly string SourceDir = Program.SourceDir;
        private readonly string SrcFileTepmlate = Program.SrcFileTepmlate;
        //private readonly string DstFileTemplate = Program.DstFileTemplate;
        //private readonly char DirSeparator = Program.DirSeparator;

        private IDataSource dataSource;

        public Restore(int epoch)
        {
            dataSource = new Db(DbFileName);

            IEnumerable<Event> events = dataSource.ListToRestore(epoch);

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

            Utils.CleanDir(SourceDir, SrcFileTepmlate);

            foreach (var item in events.Where(ev => ev.Change != (int)WatcherChangeTypes.Deleted))
            {
                DoRestore(item.Name, item.Guid, item.Version);
                Console.WriteLine($"File: {item.Name} has restored.");
            }
        }

        private void DoRestore(string name, string guid, int version)
        {
            string source =
                DestinationDir + Path.DirectorySeparatorChar + guid + "." + version.ToString();

            Directory.CreateDirectory(Path.GetDirectoryName(name));
            if (Directory.Exists(name))
            {
                ReverseDirRename(name + " - copy(1)", name);
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
                dstName = Regex.Replace(dstName, @"copy\((\d+)\)$",
                    (match) =>
                    {
                        return "copy(" + (Convert.ToInt32(match.Groups[1].Value) + 1).ToString() + ")";
                    });

                ReverseDirRename(dstName, sourceName);
            }
        }
    }
}