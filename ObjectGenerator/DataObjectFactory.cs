using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace ObjectGenerator
{
    public class DataObjectFactory
    {
        private readonly string[] _uniqueFields;

        public DataObjectFactory(IOrderedEnumerable<string> uniqueFields)
        {
            _uniqueFields = uniqueFields.ToArray();
        }

        public DataObject Create(JObject jsonObject)
        {
            foreach(var o in jsonObject)
            {
                var fieldName = o.Key;
                _ = o.Value.Type == JTokenType.String;
                var val = o.Value.Value<string>();
            }

            return new DataObject(Guid.Empty, string.Empty, new [] {new DataObjectFieldDescriptor("dummy", DataObjectFieldType.String, DataObjectFieldTypeFlags.None, 0, 0)}, new byte[1]);
        }
    }
}