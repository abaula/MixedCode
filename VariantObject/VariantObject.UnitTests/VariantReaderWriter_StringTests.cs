using Xunit;

namespace VariantObject.UnitTests
{
    public class VariantReaderWriter_StringTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("абвгдеё")]
        [InlineData("АБВГДЕЁ")]
        [InlineData("аБвГдЕё")]
        [InlineData("1234567890-=+?№")]
        [InlineData(".\"'{}[]|\\/")]
        public void String_WriteRead_Success(string expected)
        {
            var variant = VariantWriter.ToVariant(expected);
            var actual = VariantReader.ToStringValue(variant);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StringArray_WriteRead_Success()
        {
            var expected = new[]
            {
                "",
                null,
                "абвгдеё",
                null
            };

            var variant = VariantWriter.ToVariantArray(expected);
            var actual = VariantReader.ToStringArray(variant);

            Assert.Equal(expected.Length, actual.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}
