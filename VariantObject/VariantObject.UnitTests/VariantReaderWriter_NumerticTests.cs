using Xunit;

namespace VariantObject.UnitTests
{
    public class VariantReaderWriter_NumerticTests
    {
        [Fact]
        public void SByte_WriteRead_Success()
        {
            foreach (var expected in new sbyte[] { sbyte.MinValue, 0, sbyte.MaxValue })
            {
                var variant = VariantWriter.ToVariant<sbyte>(expected);
                var actual = VariantReader.ToValue<sbyte>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Byte_WriteRead_Success()
        {
            foreach (var expected in new byte[] { byte.MinValue, byte.MaxValue })
            {
                var variant = VariantWriter.ToVariant<byte>(expected);
                var actual = VariantReader.ToValue<byte>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Short_WriteRead_Success()
        {
            foreach (var expected in new short[] { short.MinValue, 0, short.MaxValue })
            {
                var variant = VariantWriter.ToVariant<short>(expected);
                var actual = VariantReader.ToValue<short>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void UShort_WriteRead_Success()
        {
            foreach (var expected in new ushort[] { ushort.MinValue, ushort.MaxValue })
            {
                var variant = VariantWriter.ToVariant<ushort>(expected);
                var actual = VariantReader.ToValue<ushort>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Int_WriteRead_Success()
        {
            foreach (var expected in new int[] { int.MinValue, 0, int.MaxValue })
            {
                var variant = VariantWriter.ToVariant<int>(expected);
                var actual = VariantReader.ToValue<int>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void UInt_WriteRead_Success()
        {
            foreach (var expected in new uint[] { uint.MinValue, uint.MaxValue })
            {
                var variant = VariantWriter.ToVariant<uint>(expected);
                var actual = VariantReader.ToValue<uint>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void Long_WriteRead_Success()
        {
            foreach (var expected in new long[] { long.MinValue, 0, long.MaxValue })
            {
                var variant = VariantWriter.ToVariant<long>(expected);
                var actual = VariantReader.ToValue<long>(variant);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void ULong_WriteRead_Success()
        {
            foreach (var expected in new ulong[] { ulong.MinValue, ulong.MaxValue })
            {
                var variant = VariantWriter.ToVariant<ulong>(expected);
                var actual = VariantReader.ToValue<ulong>(variant);

                Assert.Equal(expected, actual);
            }
        }
    }
}
