using System;
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
                switch (o.Value.Type)
                {
                    case JTokenType.String:
                        fields.Add(CreateString(o.Key, o.Value));
                        break;

                    case JTokenType.Integer:
                        fields.Add(CreateInt(o.Key, o.Value));
                        break;

                    case JTokenType.Float:
                        fields.Add(CreateFloat(o.Key, o.Value));
                        break;                        

                    default:
                        break;
                }
            }

            return new VO.VariantObject(string.Empty, fields.ToArray());
        }

        private static VO.Field CreateFloat(string fieldName, JToken token)
        {
            var val = token.Value<float>();
            var variantVal = VO.VariantWriter.ToVariant(val);

            return new VO.Field(fieldName, variantVal);
        }

        private static VO.Field CreateInt(string fieldName, JToken token)
        {
            var val = token.Value<int>();
            var variantVal = VO.VariantWriter.ToVariant(val);

            return new VO.Field(fieldName, variantVal);
        }

        private static VO.Field CreateString(string fieldName, JToken token)
        {
            var val = token.Value<string>();
            var variantVal = VO.VariantWriter.ToVariant(val);

            return new VO.Field(fieldName, variantVal);
        }
    }
}