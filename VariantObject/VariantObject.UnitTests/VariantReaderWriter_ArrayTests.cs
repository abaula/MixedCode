using Xunit;

namespace VariantObject.UnitTests
{
    public class VariantReaderWriter_ArrayTests
    {
        [Fact]
        public void CharArray_WriteRead_Success()
        {
            var expected = new[] 
            {
                'а',
                'б',
                'в',
                'г'
            };

            var variant = VariantWriter.ToVariantArray(expected);
            var actual = VariantReader.ToCharArray(variant);

            Assert.Equal(expected.Length, actual.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        [Fact]
        public void NullableCharArray_WriteRead_Success()
        {
            var expected = new char?[] 
            {
                'а',
                null,
                'б',
                'в',
                'г'
            };

            var variant = VariantWriter.ToVariantArray<char>(expected);
            var actual = VariantReader.ToNullableValueArray<char>(variant);

            Assert.Equal(expected.Length, actual.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}
