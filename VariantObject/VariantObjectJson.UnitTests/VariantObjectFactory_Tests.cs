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
        }

        [Fact]
        public void Create_Success()
        {
            var sample = new SampleObject
            {
                Id = Guid.NewGuid().ToString(),
                Name = Guid.NewGuid().ToString()
            };

            var jObject = JObject.FromObject(sample);
            var variantObject = VariantObjectFactory.Create(jObject);

            var idActual = VariantReader.ToStringValue(variantObject.Fields[0].Value);
            var nameActual = VariantReader.ToStringValue(variantObject.Fields[1].Value);

            Assert.Equal(sample.Id, idActual);
            Assert.Equal(sample.Name, nameActual);
        }
    }
}
