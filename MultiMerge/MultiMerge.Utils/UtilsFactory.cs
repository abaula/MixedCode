using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Utils
{
    public static class UtilsFactory
    {
        public static IFileReader CreateFileReader()
        {
            return new FileReader();
        }

        public static IFileWriter CreateFileWriter()
        {
            return new FileWriter();
        }
    }
}
