using System.Collections.Generic;

namespace MultiMerge.Model
{
    public interface ITextObject
    {
        string Source { get; set; }
        List<int> Lines { get; } 
    }
}
