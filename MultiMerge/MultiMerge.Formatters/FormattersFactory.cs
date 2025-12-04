using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.Formatters
{
    public static class FormattersFactory
    {
        public static IDiffObjectFormatter CreateDiffObjectFormatter(DiffObjectFormatterType type)
        {
            if(type == DiffObjectFormatterType.Simple)
                return new SimpleDiffObjectFormatter();

            throw new ArgumentException("Указанный тип форматера не поддерживается.");
        }

        public static IMergedObjectFormatter CreateMergedObjectFormatter(MergedObjectFormatterType type)
        {
            if (type == MergedObjectFormatterType.Simple)
                return new SimpleMergedObjectFormatter();

            throw new ArgumentException("Указанный тип форматера не поддерживается.");            
        }
    }
}
