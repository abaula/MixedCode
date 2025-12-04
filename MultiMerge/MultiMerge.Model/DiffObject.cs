using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    class DiffObject : IDiffObject
    {
        public ITextObject OriginalText { get; set; }
        public ITextObject VersionText { get; set; }
        public List<IDiffObjectLine> DiffLines { get; private set; }

        public DiffObject()
        {
            DiffLines = new List<IDiffObjectLine>();
        }
    }
}
