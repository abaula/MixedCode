using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Model
{
    public static class ModelFactory
    {
        public static IUniqueTextLinesStorage CreateUniqueFileLinesStorage()
        {
            return new UniqueTextLinesStorage();
        }

        public static ITextObjectBuilder CreateFileObjectBuilder()
        {
            return new TextObjectBuilder();
        }

        public static IDiffObjectBuilder CreateDiffObjectBuilder()
        {
            return new DiffObjectBuilder();
        }

        public static IMergedObjectBuilder CreateMergedObjectBuilder()
        {
            return new MergedObjectBuilder();
        }

        internal static ITextObject CreateFileObject()
        {
            return new TextObject();
        }

        internal static IDiffObject CreateDiffObject()
        {
            return new DiffObject();
        }

        internal static IDiffObjectLine CreateDiffObjectLine()
        {
            return new DiffObjectLine();
        }

        internal static MergedObjectBlock CreateMergedObjectBlock()
        {
            return new MergedObjectBlock();
        }

        internal static IMergedObject CreateMergedObject()
        {
            return new MergedObject();
        }
    }
}
