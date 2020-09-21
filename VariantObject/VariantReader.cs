using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace VariantObject
{
    public static class VariantReader
    {
        private static readonly UTF8Encoding Utf8Encoding = new UTF8Encoding(false, true);

        public static T ToValue<T>(this Variant variant)
            where T : unmanaged
        {
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

        public static T[] ToValueArray<T>(this Variant variant)
            where T : unmanaged
        {
            CheckVariantIsArrayOrThrow(variant);
            CheckTypeOrThrow(typeof(T), variant);

            if (variant.Data == null)
                return null;

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(variant.Data);
            stream.Position = 0;

            var length = stream.Read<int>();

            if (length < 1)
                return Array.Empty<T>();

            var results = new T[length];
            var tSpan = results.AsSpan();
            var span = MemoryMarshal.AsBytes(tSpan);
            stream.Read(span);

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