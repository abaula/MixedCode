using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace VariantObject
{
    public static class VariantWriter
    {
        private static readonly UTF8Encoding Utf8Encoding = new UTF8Encoding(false, true);

        public static Variant ToVariant<T>(T value)
            where T : unmanaged
        {
            if (typeof(T) == typeof(string))
                throw new ArgumentException($"Use overload {nameof(ToVariant)}(string) method for string values.");

            if (EqualityComparer<T>.Default.Equals(value, default))
                throw new ArgumentException($"Use {nameof(ToNullVariant)} method for default values.");

            using var stream = MemoryStreamResource.GetStream();
            var tSpan = MemoryMarshal.CreateSpan(ref value, 1);
            var span = MemoryMarshal.AsBytes(tSpan);
            stream.Write(span);

            return new Variant(GetType(typeof(T)), stream.ToArray());
        }

        public static Variant ToNullVariant<T>() => new Variant(GetType(typeof(T)), null);

        public static Variant ToVariantArray<T>(T[] values)
            where T : unmanaged
        {
            if (typeof(T) == typeof(string))
                throw new ArgumentException($"Use overload {nameof(ToVariantArray)}(string[]) method for string values.");

            if (values == null)
                throw new ArgumentException($"Use {nameof(ToNullVariant)} method for default values.");

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(values.Length);

            if (values.Length > 0)
            {
                var tSpan = values.AsSpan();
                var span = MemoryMarshal.AsBytes(tSpan);
                stream.Write(span);
            }

            return new Variant(GetType(typeof(T)), stream.ToArray());
        }

        public static Variant ToVariant(string value)
        {
            if (value == null)
                throw new ArgumentException($"Use {nameof(ToNullVariant)} method for default values.");

            using var stream = MemoryStreamResource.GetStream();
            var valueSpan = value.AsSpan();
            var length = Utf8Encoding.GetByteCount(valueSpan);

            Span<byte> byteSpan = stackalloc byte[length];
            var encodedLength = Utf8Encoding.GetBytes(valueSpan, byteSpan);

            stream.Write(encodedLength);
            stream.Write(byteSpan);

            return new Variant(VariantType.String, stream.ToArray());
        }

        public static Variant ToVariantArray(string[] values)
        {
            if (values == null)
                throw new ArgumentException($"Use {nameof(ToNullVariant)} method for default values.");

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(values.Length);

            if (values.Length > 0)
            {
                foreach (var value in values)
                {
                    var valueSpan = value.AsSpan();
                    var length = Utf8Encoding.GetByteCount(valueSpan);

                    Span<byte> byteSpan = stackalloc byte[length];
                    var encodedLength = Utf8Encoding.GetBytes(valueSpan, byteSpan);

                    stream.Write(encodedLength);
                    stream.Write(byteSpan);
                }
            }

            return new Variant(VariantType.String | VariantType.Array, stream.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static VariantType GetType(Type type)
        {
            return VariantType.None;
        }
    }
}