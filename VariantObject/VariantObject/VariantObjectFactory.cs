using System.Linq;
using Newtonsoft.Json.Linq;

namespace VariantObject
{
    public class VariantObjectFactory
    {
        private readonly string[] _uniqueFields;

        public VariantObjectFactory(IOrderedEnumerable<string> uniqueFields)
        {
            _uniqueFields = uniqueFields.ToArray();
        }

        public VariantObject Create(JObject jsonObject)
        {
            if(jsonObject == null)
                return VariantObject.Empty;

            foreach (var o in jsonObject)
            {
                var fieldName = o.Key;
                _ = o.Value.Type == JTokenType.String;
                var val = o.Value.Value<string>();
            }

            return new VariantObject(string.Empty, new[] { new Field() });
        }
    }
}