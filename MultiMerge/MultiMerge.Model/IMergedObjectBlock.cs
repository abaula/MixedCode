using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    public interface IMergedObjectBlock
    {
        DiffState State { get; set; }
        ITextObject Source { get; set; }
        int OriginalTextBottomBorderLineNumber { get; set; }
        int OriginalTextTopBorderLineNumber { get; set; }
        List<IDiffObjectLine> DiffLines { get; }
    }
}
