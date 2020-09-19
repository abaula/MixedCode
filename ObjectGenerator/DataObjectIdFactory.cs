using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectGenerator
{
    public class DataObjectIdFactory
    {
        private readonly string[] _uniqueFields;

        public DataObjectIdFactory(IOrderedEnumerable<string> uniqueFields)
        {
            _uniqueFields = uniqueFields.ToArray();
        }

        public Guid CreateId(DataObject dataObject)
        {
            var buffer = new List<byte>();
            
            foreach(var fileldKey in _uniqueFields)
            {
                var fileldValue = DataObjectHelper.GetValue(dataObject, fileldKey);
                buffer.AddRange(fileldValue.Data);
            }

            return ByteConverter.BytesToMd5HashGuid(buffer.ToArray());
        }
    }
}