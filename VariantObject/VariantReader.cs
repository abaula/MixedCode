using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace VariantObject
{
    public static class VariantReader
    {
        private static readonly UTF8Encoding Utf8Encoding = new UTF8Encoding(false, true);

        public static T ToValue<T>(Variant variant)
            where T : unmanaged
        {
            if (typeof(T) == typeof(string))
                throw new ArgumentException($"Use {nameof(ToStringValue)} method for string values.");

            CheckTypeOrThrow(typeof(T), variant);

            if (variant.Data == null)
                return default;

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(variant.Data);
            stream.Position = 0;

            var result = default(T);
            var tSpan = MemoryMarshal.CreateSpan(ref result, 1);
            var span = MemoryMarshal.AsBytes(tSpan);
            stream.Read(span);

            return result;
        }

        public static T[] ToValueArray<T>(Variant variant)
            where T : unmanaged
        {
            if (typeof(T) == typeof(string))
                throw new ArgumentException($"Use {nameof(ToStringArray)} method for string values.");

            if (typeof(T) == typeof(char))
                throw new ArgumentException($"Use {nameof(ToCharArray)} method for char values.");

            CheckVariantIsArrayOrThrow(variant);
            CheckTypeOrThrow(typeof(T), variant);

            if (variant.Data == null)
                return null;

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(variant.Data);
            stream.Position = 0;

            var length = stream.Read<int>();

            if (length == 0)
                return Array.Empty<T>();

            var results = new T[length];
            var tSpan = results.AsSpan();
            var span = MemoryMarshal.AsBytes(tSpan);
            stream.Read(span);

            return results;
        }

        public static string ToStringValue(Variant variant)
        {
            CheckTypeOrThrow(typeof(string), variant);

            if (variant.Data == null)
                return null;

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(variant.Data);
            stream.Position = 0;

            var byteLength = stream.Read<int>();

            if (byteLength == 0)
                return string.Empty;

            Span<byte> bytes = stackalloc byte[byteLength];
            stream.Read(bytes);

            return Utf8Encoding.GetString(bytes);
        }

        public static string[] ToStringArray(Variant variant)
        {
            CheckVariantIsArrayOrThrow(variant);
            CheckTypeOrThrow(typeof(string), variant);

            if (variant.Data == null)
                return null;

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(variant.Data);
            stream.Position = 0;

            var arrayLength = stream.Read<int>();

            if (arrayLength == 0)
                return Array.Empty<string>();

            var result = new string[arrayLength];

            for (var i = 0; i < arrayLength; i++)
            {
                var byteLength = stream.Read<int>();

                if (byteLength == -1)
                {
                    result[i] = null;
                    continue;
                }

                if (byteLength == 0)
                {
                    result[i] = string.Empty;
                    continue;
                }

                Span<byte> bytes = stackalloc byte[byteLength];
                stream.Read(bytes);
                result[i] = Utf8Encoding.GetString(bytes);
            }

            return result;
        }

        public static char[] ToCharArray(Variant variant)
        {
            CheckVariantIsArrayOrThrow(variant);
            CheckTypeOrThrow(typeof(char), variant);

            if (variant.Data == null)
                return null;

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(variant.Data);
            stream.Position = 0;

            var arrayLength = stream.Read<int>();

            if (arrayLength == 0)
                return Array.Empty<char>();

            var byteLength = stream.Read<int>();
            Span<byte> span = stackalloc byte[byteLength];
            stream.Read(span);

            var results = new char[arrayLength];
            var charSpan = results.AsSpan();
            Utf8Encoding.GetChars(span, charSpan);

            return results;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckTypeOrThrow(Type type, Variant variant)
        {

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckVariantIsArrayOrThrow(Variant variant)
        {
            if (variant.Type.HasFlag(VariantType.Array))
                return;

            throw new InvalidOperationException("Variant type is not array.");
        }
    }
}