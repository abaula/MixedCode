using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    class TextObjectBuilder : ITextObjectBuilder
    {
        public ITextObject BuildTextObjectFromReader(TextReader reader, string source, IUniqueTextLinesStorage storage)
        {
            string line;
            var fileObject = ModelFactory.CreateFileObject();
            fileObject.Source = source;

            while ((line = reader.ReadLine()) != null)
            {
                var code = storage.AddLine(line);
                fileObject.Lines.Add(code);
            }

            return fileObject;
        }
    }
}
