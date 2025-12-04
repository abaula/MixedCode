using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Utils
{
    class FileReader : IFileReader
    {
        public StringBuilder GetTextFromFile(string path)
        {
            var sb = new StringBuilder();

            using (var reader = new StreamReader(path, DefaultEncoding.Encoding))
            {
                sb.Append(reader.ReadToEnd());
            }

            return sb;
        }
    }
}
