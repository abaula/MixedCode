using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Utils
{
    class FileWriter : IFileWriter
    {
        public void WriteTextToFile(StringBuilder text, string path, bool append)
        {
            using (TextWriter writer = new StreamWriter(path, append, Encoding.Default))
            {
                writer.Write(text.ToString());
            }
        }
    }
}
