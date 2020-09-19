using System;
using System.Linq;

namespace ObjectGenerator
{
    public static class DataObjectHelper
    {
        public static DataObjectValue GetValue(DataObject source, string fieldKey)
        {
            var field = source.Fileds.SingleOrDefault(_ => _.Key == fieldKey);

            if(field == DataObjectFieldDescriptor.Empty)
                throw new InvalidOperationException($"Field '{fieldKey}' is not found.");

            var span = new ReadOnlySpan<byte>(source.Data, field.Offset, field.Length);

            return new DataObjectValue(field, span.ToArray());
        }
    }
}