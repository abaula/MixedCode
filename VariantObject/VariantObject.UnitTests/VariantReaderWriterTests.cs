using System;
using Xunit;

namespace VariantObject.UnitTests
{
    public class VariantReaderWriterTests
    {
        [Fact]
        public void Int_WrireRead_Success()
        {
            for (var i = 0; i < 100; i++)
            {
                var variant = VariantWriter.ToVariant<int>(i);
                var value = VariantReader.ToValue<int>(variant);

                Assert.Equal(i, value);
            }
        }
    }
}
