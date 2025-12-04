using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    class TextObject : ITextObject
    {
        public string Source { get; set; }
        public List<int> Lines { get; private set; }

        public TextObject()
        {
            Lines = new List<int>();
        }
    }
}
