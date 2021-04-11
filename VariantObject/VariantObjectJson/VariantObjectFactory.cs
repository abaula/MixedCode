using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using VO = VariantObject;

namespace VariantObjectJson
{
    public static class VariantObjectFactory
    {
        public static VO.VariantObject Create(JObject jsonObject)
        {
            if (jsonObject == null)
                return VO.VariantObject.Empty;

            var fields = new List<VO.Field>();

            foreach (var o in jsonObject)
            {
                var fieldName = o.Key;
                // TODO пока отлаживаемся на типе string.
                _ = o.Value.Type == JTokenType.String;
                var val = o.Value.Value<string>();

                var variantVal = VO.VariantWriter.ToVariant(val);
                fields.Add(new VO.Field(fieldName, variantVal));
            }

            return new VO.VariantObject(string.Empty, fields.ToArray());
        }
    }
}