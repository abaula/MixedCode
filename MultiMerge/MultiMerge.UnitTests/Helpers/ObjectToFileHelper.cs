using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiMerge.Formatters;
using MultiMerge.Model;
using MultiMerge.Utils;

namespace MultiMerge.UnitTests.Helpers
{
    class ObjectToFileHelper
    {
        public static void WriteDiffObjectToFile(IDiffObject diffObject, IUniqueTextLinesStorage storage, string filePath, bool open)
        {
            var simpleFormatter = FormattersFactory.CreateDiffObjectFormatter(DiffObjectFormatterType.Simple);
            var sb = simpleFormatter.GetFormattedText(diffObject, storage);

            var writer = UtilsFactory.CreateFileWriter();
            writer.WriteTextToFile(sb, filePath, false);

            if(open)
                Process.Start("notepad.exe", filePath);
        }


        public static void WriteMergedObjectToFile(IMergedObject mergedObject, IUniqueTextLinesStorage storage, string filePath, bool open)
        {
            var simpleFormatter = FormattersFactory.CreateMergedObjectFormatter(MergedObjectFormatterType.Simple);
            var sb = simpleFormatter.GetFormattedText(mergedObject, storage);

            var writer = UtilsFactory.CreateFileWriter();
            writer.WriteTextToFile(sb, filePath, false);

            if (open)
                Process.Start("notepad.exe", filePath);
        }
    }
}
