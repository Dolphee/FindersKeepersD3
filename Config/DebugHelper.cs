using System;
using System.Linq;
using System.IO;

namespace FindersKeepers.DebugWorker
{
    public class DebugWriter
    {
        public static DebugWriter Instance { get { return _instance; } }
        public static DebugWriter _instance = new DebugWriter();
        private string FKDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private string ErrorFolder = "FKError\\";

        private enum ErrorTypes : uint
        {
            E_FAIL = 0x80004005, /* Bad memory address */
            E_POINTER = 0x80004003 /* Null references */
        }

        private string GetSlug(string DirectoryName) {
            return string.Format("{0}{1}{2}", FKDirectory, ErrorFolder, DirectoryName);
        }

        private void CreateDirectory(string DirectoryName)
        {
            string slug = GetSlug(DirectoryName);

            if (!File.Exists(slug))
                Directory.CreateDirectory(slug);
        }

        public static void Write(Exception e)
        {
            /* Skip if not in debug mode */
            if (!Config._.FKConfig.General.FKSettings.DebugMode)
                return;

            Instance.CreateDirectory(""); /* Create root FKError folder */

            bool knownValue = Enum.GetValues(typeof(ErrorTypes)).Cast<uint>().ToList().Contains((uint)e.HResult);
            string DirectorySlug = Instance.GetSlug("");
            int eHash = e.ToString().GetHashCode();

            if (knownValue)
            {
                ErrorTypes Error = (ErrorTypes)e.HResult;
                Instance.CreateDirectory(Error.ToString());
                DirectorySlug = Instance.GetSlug(Error.ToString());
            }

            string fileSlug = string.Format("{0}\\{1}.FKDump", DirectorySlug, eHash);

            if (!File.Exists(fileSlug))
                using (StreamWriter fs = new StreamWriter(fileSlug))
                    fs.WriteLine(e);
        }


    }
}
