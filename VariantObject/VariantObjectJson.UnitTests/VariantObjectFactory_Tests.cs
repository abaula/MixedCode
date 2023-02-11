using System;
using Newtonsoft.Json.Linq;
using VariantObject;
using Xunit;

namespace VariantObjectJson.UnitTests
{
    public class VariantObjectFactory_Tests
    {
        private class SampleObject
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int IntNumber { get; set; }
            public float FloatNumber { get; set; }
        }

        [Fact]
        public void Create_Success()
        {
            var random = new Random();
            var sample = new SampleObject
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString(),
                IntNumber = random.Next(),
                FloatNumber = (float)random.NextDouble()
            };

            var jObject = JObject.FromObject(sample);
            var variantObject = VariantObjectFactory.Create(jObject);

            var idActual = VariantReader.ToStringValue(variantObject.Fields[0].Value);
            var nameActual = VariantReader.ToStringValue(variantObject.Fields[1].Value);
            var intNumberActual = VariantReader.ToValue<int>(variantObject.Fields[2].Value);
            var floatNumberActual = VariantReader.ToValue<float>(variantObject.Fields[3].Value);

            Assert.Equal(sample.Id, idActual);
            Assert.Equal(sample.Name, nameActual);
            Assert.Equal(sample.IntNumber, intNumberActual);
            Assert.Equal(sample.FloatNumber, floatNumberActual);
        }
    }
}
