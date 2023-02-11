using System;
using Xunit;

namespace VariantObject.UnitTests
{
    public class VariantReaderWriter_CharTests
    {
        [Fact]
        public void Char_WriteRead_Success()
        {
            foreach (var expected in new char[] { char.MinValue, char.MaxValue })
            {
                var variant = VariantWriter.ToVariant<char>(expected);
                var actual = VariantReader.ToValue<char>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Char_WriteRead_Throws()
        {
            var variant = VariantWriter.ToVariant<char>(char.MinValue);
            Assert.Throws<InvalidOperationException>(() => VariantReader.ToNullableValue<char>(variant));
        }

        [Fact]
        public void Char_WriteRead_Nullable_Success()
        {
            var variant = VariantWriter.ToVariant<char>(null);
            var actual = VariantReader.ToNullableValue<char>(variant);
            Assert.Null(actual);
        }

        [Fact]
        public void Char_WriteRead_Nullable_Throws()
        {
            var variant = VariantWriter.ToVariant<char>(null);
            Assert.Throws<InvalidOperationException>(() => VariantReader.ToValue<char>(variant));
        }

        [Theory]
        [InlineData(char.MinValue, char.MaxValue)]
        public void CharArray_WriteRead_Success(params char[] expected)
        {
            var variant = VariantWriter.ToVariantArray(expected);
            var actual = VariantReader.ToCharArray(variant);

            Assert.Equal(expected.Length, actual.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        [Fact]
        public void CharArray_Empty_WriteRead_Success()
        {
            var expected = Array.Empty<char>();

            var variant = VariantWriter.ToVariantArray(expected);
            var actual = VariantReader.ToCharArray(variant);

            Assert.Empty(actual);
        }

        [Fact]
        public void CharArray_Null_WriteRead_Success()
        {
            var expected = (char[])null;

            var variant = VariantWriter.ToVariantArray(expected);
            var actual = VariantReader.ToCharArray(variant);

            Assert.Null(actual);
        }        

        [Theory]
        [InlineData(char.MinValue, char.MaxValue)]
        [InlineData(char.MinValue, null, char.MaxValue)]
        [InlineData(null, char.MinValue, null, char.MaxValue)]
        [InlineData(null, char.MinValue, null, char.MaxValue, null)]
        [InlineData(null, char.MinValue, null, null, char.MaxValue, null)]
        [InlineData(null, null, char.MinValue, char.MaxValue, null)]
        [InlineData(null, null)]
        [InlineData(null)]
        public void NullableCharArray_WriteRead_Success(params char?[] expected)
        {
            if (expected == null)
                expected = new char?[] { null };

            var variant = VariantWriter.ToVariantArray<char>(expected);
            var actual = VariantReader.ToNullableValueArray<char>(variant);

            Assert.Equal(expected.Length, actual.Length);

            for (var i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        [Fact]
        public void NullableCharArray_Empty_WriteRead_Success()
        {
            var expected = Array.Empty<char?>();

            var variant = VariantWriter.ToVariantArray(expected);
            var actual = VariantReader.ToNullableValueArray<char>(variant);

            Assert.Empty(actual);
        }

        [Fact]
        public void NullableCharArray_Null_WriteRead_Success()
        {
            var expected = (char?[])null;

            var variant = VariantWriter.ToVariantArray(expected);
            var actual = VariantReader.ToNullableValueArray<char>(variant);

            Assert.Null(actual);
        }  
    }
}
