using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiMerge.Model;

namespace MultiMerge.Formatters
{
    public interface IMergedObjectFormatter
    {
        StringBuilder GetFormattedText(IMergedObject mergedObject, IUniqueTextLinesStorage storage);
    }
}
