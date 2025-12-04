using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    class DiffObjectLine : IDiffObjectLine
    {
        public DiffState LineState { get; set; }
        public int LineCode { get; set; }
        public ITextObject SourceText { get; set; }
        public int SourceTextLineNumber { get; set; }
    }
}
