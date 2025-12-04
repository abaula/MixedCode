using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    class MergedObject : IMergedObject
    {
        public ITextObject OriginalText { get; set; }
        public List<ITextObject> VersionTexts { get; private set; }
        public List<IMergedObjectBlock> Blocks { get; private set; }

        public MergedObject()
        {
            VersionTexts = new List<ITextObject>();
            Blocks = new List<IMergedObjectBlock>();
        }
    }
}
