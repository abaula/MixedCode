using System.Collections.Generic;

namespace MultiMerge.Model
{
    public interface IMergedObject
    {
        ITextObject OriginalText { get; set; }
        List<ITextObject> VersionTexts { get; }
        List<IMergedObjectBlock> Blocks { get; } 
    }
}
