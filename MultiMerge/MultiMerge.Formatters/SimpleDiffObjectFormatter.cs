using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiMerge.Model;

namespace MultiMerge.Formatters
{
    class SimpleDiffObjectFormatter : IDiffObjectFormatter
    {
        public StringBuilder GetFormattedText(IDiffObject diffObject, IUniqueTextLinesStorage storage)
        {
            var sb = new StringBuilder();

            foreach (var diffLine in diffObject.DiffLines)
            {
                string prefix = " ";

                if (diffLine.LineState == DiffState.Added)
                    prefix = "+";
                else if (diffLine.LineState == DiffState.Deleted)
                    prefix = "-";

                sb.AppendFormat("{0}\t{1} {2}", diffLine.SourceTextLineNumber, prefix, storage.GetLine(diffLine.LineCode));
                sb.AppendLine();
            }

            return sb;
        }
    }
}
