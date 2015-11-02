namespace _5_01
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Reflection;

    internal class Program
    {
        public static readonly string SrcFileTepmlate = "*.txt";
        public static readonly string DstFileTemplate = "????????-????-????-????-????????????.*";
        public static readonly string DateFormat = "d.MM.yyyy-HH:mm:ss";
        private static readonly string AssemblyName = Assembly.GetCallingAssembly().GetName().Name;
        public static readonly char DirSeparator = Path.DirectorySeparatorChar;
        public static readonly string DbFileName = ConfigurationManager.AppSettings["DbFileName"];
        public static readonly string DestinationDir = ConfigurationManager.AppSettings["DestinationDir"].TrimEnd(DirSeparator);
        public static readonly string SourceDir = ConfigurationManager.AppSettings["SourceDir"].TrimEnd(DirSeparator);
        private static readonly string OptRestore = "--restore";
        private static readonly string OptListAll = "--listall";
        private static readonly string OptDestroy = "--destroy";

        private static readonly string Usage =
            "Usage:\n" +
            "For watching:\n" +
            $"\tJust launch it without arguments and it'll start for watching {SourceDir}\n" +
            "For restore files:\n" +
            $"\t{AssemblyName} {OptRestore} {DateFormat}\n" +
            $"For print all log:\n\t{AssemblyName} {OptListAll}\n" +
            $"For delete all backups use:\n\t{AssemblyName} {OptDestroy}";

        private static void Main(string[] args)
        {
            int epoch;

            if (args.Length == 0)
            {
                new Watch();
            }
            else if (args.Length == 1 && args[0] == OptListAll)
            {
                ListAll();
            }
            else if (args.Length == 1 && args[0] == OptDestroy)
            {
                Destroy();
            }
            else if (args.Length == 2 && args[0] == OptRestore && Utils.String2Epoch(args[1], out epoch))
            {
                new Restore(epoch);
            }
            else
            {
                Console.WriteLine(Usage);
            }
        }

        private static void Destroy()
        {
            Console.WriteLine("Are you sure? Y/N");
            string answer = Console.ReadLine();
            if (answer.Equals("y", StringComparison.InvariantCultureIgnoreCase))
            {
                File.Delete(DbFileName);
                Utils.CleanDir(DestinationDir, DstFileTemplate);

                Environment.Exit(0);
            }
        }

        private static void ListAll()
        {
            IDataSource dataSource = new Db(DbFileName);

            foreach (var item in dataSource.ListAll())
            {
                Console.WriteLine(
                    $"File: {item.Name}; Change: {Enum.GetName(typeof(WatcherChangeTypes), item.Change)}; Time: {Utils.Epoch2String(item.Date)}");
            }
        }
    }
}