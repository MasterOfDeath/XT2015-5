namespace _5_01
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;

    internal class Utils
    {
        private static readonly DateTime OriginDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly string DateFormat = Program.DateFormat;

        public static bool IsDirEmpty(string dirName, string fileTepmlate)
        {
            return !Directory.EnumerateFiles(dirName, fileTepmlate, SearchOption.AllDirectories).Any();
        }

        public static void CleanDir(string dirName, string fileTepmlate)
        {
            foreach (var file in Directory.EnumerateFiles(dirName, fileTepmlate, SearchOption.AllDirectories))
            {
                File.Delete(file);
            }
        }

        public static bool IsDir(string fileName)
        {
            FileInfo file = new FileInfo(fileName);
            return file.Attributes == FileAttributes.Directory;
        }

        public static int GetNowInEpoch()
        {
            return (int)(DateTime.Now - OriginDate).TotalSeconds;
        }

        public static string Epoch2String(int epoch)
        {
            return OriginDate.AddSeconds(epoch).ToString();
        }

        public static bool String2Epoch(string strDate, out int epoch)
        {
            bool result;

            DateTime date = DateTime.Now;
            result = DateTime.TryParseExact(
                strDate,
                DateFormat,
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out date);

            epoch = (int)(date - OriginDate).TotalSeconds;

            return result;
        }
    }
}