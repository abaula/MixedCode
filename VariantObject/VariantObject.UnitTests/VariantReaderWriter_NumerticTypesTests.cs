using System;
using Xunit;

namespace VariantObject.UnitTests
{
    public class VariantReaderWriter_NumerticTypesTests
    {

        [Fact]
        public void WrongType_WriteRead_Throws()
        {
            var types = new[]
            {
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong)
            };

            foreach (var type1 in types)
            {
                foreach (var type2 in types)
                {
                    if (type1 == type2)
                        continue;

                    var variant = GetVariant(type1);
                    ReadVariant(type2, variant);
                }
            }
        }

        private static void ReadVariant(Type type, Variant variant)
        {
            if (type == typeof(byte))
            {
                Assert.Throws<InvalidOperationException>(() => VariantReader.ToValue<byte>(variant));
                return;
            }

            if (type == typeof(sbyte))
            {
                Assert.Throws<InvalidOperationException>(() => VariantReader.ToValue<sbyte>(variant));
                return;
            }

            if (type == typeof(short))
            {
                Assert.Throws<InvalidOperationException>(() => VariantReader.ToValue<short>(variant));
                return;
            }

            if (type == typeof(ushort))
            {
                Assert.Throws<InvalidOperationException>(() => VariantReader.ToValue<ushort>(variant));
                return;
            }

            if (type == typeof(int))
            {
                Assert.Throws<InvalidOperationException>(() => VariantReader.ToValue<int>(variant));
                return;
            }

            if (type == typeof(uint))
            {
                Assert.Throws<InvalidOperationException>(() => VariantReader.ToValue<uint>(variant));
                return;
            }

            if (type == typeof(long))
            {
                Assert.Throws<InvalidOperationException>(() => VariantReader.ToValue<long>(variant));
                return;
            }

            if (type == typeof(ulong))
            {
                Assert.Throws<InvalidOperationException>(() => VariantReader.ToValue<ulong>(variant));
                return;
            }

            throw new ArgumentException(nameof(type));
        }

        private static Variant GetVariant(Type type)
        {
            if (type == typeof(byte))
                return VariantWriter.ToVariant<byte>(0);

            if (type == typeof(sbyte))
                return VariantWriter.ToVariant<sbyte>(0);

            if (type == typeof(short))
                return VariantWriter.ToVariant<short>(0);

            if (type == typeof(ushort))
                return VariantWriter.ToVariant<ushort>(0);

            if (type == typeof(int))
                return VariantWriter.ToVariant<int>(0);

            if (type == typeof(uint))
                return VariantWriter.ToVariant<uint>(0);

            if (type == typeof(long))
                return VariantWriter.ToVariant<long>(0);

            if (type == typeof(ulong))
                return VariantWriter.ToVariant<ulong>(0);

            throw new ArgumentException(nameof(type));
        }
    }
}
