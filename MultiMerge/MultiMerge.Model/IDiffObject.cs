using System.Collections.Generic;

namespace MultiMerge.Model
{
    public interface IDiffObject
    {
        ITextObject OriginalText { get; set; }
        ITextObject VersionText { get; set; }
        List<IDiffObjectLine> DiffLines { get; } 
    }
}
