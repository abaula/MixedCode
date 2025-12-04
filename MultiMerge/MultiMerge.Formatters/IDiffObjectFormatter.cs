using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiMerge.Model;

namespace MultiMerge.Formatters
{
    public interface IDiffObjectFormatter
    {
        StringBuilder GetFormattedText(IDiffObject diffObject, IUniqueTextLinesStorage storage);
    }
}
