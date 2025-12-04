using System.IO;

namespace MultiMerge.Model
{
    public interface ITextObjectBuilder
    {
        ITextObject BuildTextObjectFromReader(TextReader reader, string source, IUniqueTextLinesStorage storage);
    }
}
