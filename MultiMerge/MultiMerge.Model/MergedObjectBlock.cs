using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    class MergedObjectBlock : IMergedObjectBlock
    {
        public DiffState State { get; set; }
        public ITextObject Source { get; set; }
        public int OriginalTextBottomBorderLineNumber { get; set; }
        public int OriginalTextTopBorderLineNumber { get; set; }
        public List<IDiffObjectLine> DiffLines { get; private set; }

        public MergedObjectBlock()
        {
            State = DiffState.Unknown;
            DiffLines = new List<IDiffObjectLine>();
        }
    }
}
