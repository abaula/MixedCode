using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Utils
{
    public interface IFileWriter
    {
        void WriteTextToFile(StringBuilder text, string path, bool append);
    }
}
