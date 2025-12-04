using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.UnitTests.Helpers
{
    static class DirectoryHelper
    {
        public static string GetCurrentExeDirectory()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public static string GetTestFileFullPath(string fileName)
        {
            var currentExecutingPath = GetCurrentExeDirectory();
            var path = string.Format(@"{0}\TestFiles\{1}", currentExecutingPath, fileName);

            return path;
        }
    }
}
