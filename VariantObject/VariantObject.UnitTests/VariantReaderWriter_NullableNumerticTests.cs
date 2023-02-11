using System;
using Xunit;

namespace VariantObject.UnitTests
{
    public class VariantReaderWriter_NullableNumerticTests
    {
        [Fact]
        public void SByte_WriteRead_Success()
        {
            foreach (var expected in new sbyte?[] { sbyte.MinValue, 0, sbyte.MaxValue, null })
            {
                var variant = VariantWriter.ToVariant<sbyte>(expected);
                var actual = VariantReader.ToNullableValue<sbyte>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void SByte_WriteRead_NotNullable_Throws()
        {
            var variant = VariantWriter.ToVariant<sbyte>(0);
            Assert.Throws<InvalidOperationException>(() => VariantReader.ToNullableValue<sbyte>(variant));
        }

        [Fact]
        public void Byte_WriteRead_Success()
        {
            foreach (var expected in new byte?[] { byte.MinValue, byte.MaxValue, null })
            {
                var variant = VariantWriter.ToVariant<byte>(expected);
                var actual = VariantReader.ToNullableValue<byte>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Byte_WriteRead_NotNullable_Throws()
        {
            var variant = VariantWriter.ToVariant<byte>(0);
            Assert.Throws<InvalidOperationException>(() => VariantReader.ToNullableValue<byte>(variant));
        }

        [Fact]
        public void Short_WriteRead_Success()
        {
            foreach (var expected in new short?[] { short.MinValue, 0, short.MaxValue, null })
            {
                var variant = VariantWriter.ToVariant<short>(expected);
                var actual = VariantReader.ToNullableValue<short>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Short_WriteRead_NotNullable_Throws()
        {
            var variant = VariantWriter.ToVariant<short>(0);
            Assert.Throws<InvalidOperationException>(() => VariantReader.ToNullableValue<short>(variant));
        }

        [Fact]
        public void UShort_WriteRead_Success()
        {
            foreach (var expected in new ushort?[] { ushort.MinValue, ushort.MaxValue, null })
            {
                var variant = VariantWriter.ToVariant<ushort>(expected);
                var actual = VariantReader.ToNullableValue<ushort>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void UShort_WriteRead_NotNullable_Throws()
        {
            var variant = VariantWriter.ToVariant<ushort>(0);
            Assert.Throws<InvalidOperationException>(() => VariantReader.ToNullableValue<ushort>(variant));
        }

        [Fact]
        public void Int_WriteRead_Success()
        {
            foreach (var expected in new int?[] { int.MinValue, 0, int.MaxValue, null })
            {
                var variant = VariantWriter.ToVariant<int>(expected);
                var actual = VariantReader.ToNullableValue<int>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Int_WriteRead_NotNullable_Throws()
        {
            var variant = VariantWriter.ToVariant<int>(0);
            Assert.Throws<InvalidOperationException>(() => VariantReader.ToNullableValue<int>(variant));
        }

        [Fact]
        public void UInt_WriteRead_Success()
        {
            foreach (var expected in new uint?[] { uint.MinValue, uint.MaxValue, null })
            {
                var variant = VariantWriter.ToVariant<uint>(expected);
                var actual = VariantReader.ToNullableValue<uint>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void UInt_WriteRead_NotNullable_Throws()
        {
            var variant = VariantWriter.ToVariant<uint>(0);
            Assert.Throws<InvalidOperationException>(() => VariantReader.ToNullableValue<uint>(variant));
        }

        [Fact]
        public void Long_WriteRead_Success()
        {
            foreach (var expected in new long?[] { long.MinValue, 0, long.MaxValue, null })
            {
                var variant = VariantWriter.ToVariant<long>(expected);
                var actual = VariantReader.ToNullableValue<long>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Long_WriteRead_NotNullable_Throws()
        {
            var variant = VariantWriter.ToVariant<long>(0);
            Assert.Throws<InvalidOperationException>(() => VariantReader.ToNullableValue<long>(variant));
        }

        [Fact]
        public void ULong_WriteRead_Success()
        {
            foreach (var expected in new ulong?[] { ulong.MinValue, ulong.MaxValue, null })
            {
                var variant = VariantWriter.ToVariant<ulong>(expected);
                var actual = VariantReader.ToNullableValue<ulong>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void ULong_WriteRead_NotNullable_Throws()
        {
            var variant = VariantWriter.ToVariant<ulong>(0);
            Assert.Throws<InvalidOperationException>(() => VariantReader.ToNullableValue<ulong>(variant));
        }
    }
}
