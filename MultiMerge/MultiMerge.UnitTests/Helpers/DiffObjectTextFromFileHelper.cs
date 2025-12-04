using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiMerge.Model;
using MultiMerge.Utils;

namespace MultiMerge.UnitTests.Helpers
{
    static class DiffObjectTextFromFileHelper
    {
        public static ITextObject CreateTextObjectFromFile(string path, IUniqueTextLinesStorage storage)
        {
            var fileReader = UtilsFactory.CreateFileReader();
            var sb = fileReader.GetTextFromFile(path);

            using (TextReader reader = new StringReader(sb.ToString()))
            {
                var fileObjectBuilder = ModelFactory.CreateFileObjectBuilder();
                var fileObject = fileObjectBuilder.BuildTextObjectFromReader(reader, path, storage);

                return fileObject;
            }
        }
    }
}
