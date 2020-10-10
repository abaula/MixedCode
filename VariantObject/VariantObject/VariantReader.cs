using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            if (typeof(T) == typeof(VariantObject))
                throw new ArgumentException($"Use {nameof(ToVariantObjectValue)} method for VariantObject values.");

            CheckVariantIsNotArrayOrThrow(variant);
            CheckVariantIsNotNullableOrThrow(variant);
            CheckTypeOrThrow(typeof(T), variant);

            if (variant.Data == null)
                return default;

            return ReadValue<T>(variant);
        }

        public static T? ToNullableValue<T>(Variant variant)
            where T : unmanaged
        {
            if (typeof(T) == typeof(VariantObject))
                throw new ArgumentException($"Use {nameof(ToVariantObjectValue)} method for VariantObject values.");

            CheckVariantIsNotArrayOrThrow(variant);
            CheckVariantIsNullableOrThrow(variant);
            CheckTypeOrThrow(typeof(T), variant);

            if (variant.Data == null)
                return null;

            return ReadValue<T>(variant);
        }

        private static T ReadValue<T>(Variant variant)
            where T : unmanaged
        {
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
            if (typeof(T) == typeof(char))
                throw new ArgumentException($"Use {nameof(ToCharArray)} method for char values.");

            if (typeof(T) == typeof(VariantObject))
                throw new ArgumentException($"Use {nameof(ToVariantObjectArray)} method for VariantObject values.");

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
            CheckVariantIsNotArrayOrThrow(variant);
            CheckTypeOrThrow(typeof(string), variant);

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(variant.Data);
            stream.Position = 0;

            return ReadString(stream);
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
                var strValue = ReadString(stream);
                result[i] = strValue;
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

        public static VariantObject ToVariantObjectValue(Variant variant)
        {
            CheckVariantIsNotArrayOrThrow(variant);
            CheckTypeOrThrow(typeof(VariantObject), variant);

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(variant.Data);
            stream.Position = 0;

            return ReadVariantObject(stream);
        }

        public static VariantObject[] ToVariantObjectArray(Variant variant)
        {
            CheckVariantIsArrayOrThrow(variant);
            CheckTypeOrThrow(typeof(VariantObject), variant);

            if (variant.Data == null)
                return null;

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(variant.Data);
            stream.Position = 0;

            var itemsLength = stream.Read<int>();

            if (itemsLength == 0)
                return Array.Empty<VariantObject>();

            var items = new List<VariantObject>();

            for (var i = 0; i < itemsLength; i++)
            {
                var variantObject = ReadVariantObject(stream);
                items.Add(variantObject);
            }

            return items.ToArray();
        }

        private static VariantObject ReadVariantObject(MemoryStream stream)
        {
            var type = ReadString(stream);
            var fieldsCount = stream.Read<int>();

            if (fieldsCount == -1)
                return new VariantObject(type, null);

            if (fieldsCount == 0)
                return new VariantObject(type, Array.Empty<Field>());

            var fields = new List<Field>();

            for (var i = 0; i < fieldsCount; i++)
            {
                var field = ReadField(stream);
                fields.Add(field);
            }

            return new VariantObject(type, fields.ToArray());
        }

        private static Field ReadField(MemoryStream stream)
        {
            var key = ReadString(stream);
            var type = stream.Read<VariantType>();
            var byteLength = stream.Read<int>();
            byte[] bytes;

            if (byteLength == -1)
                bytes = null;
            else if (byteLength == 0)
                bytes = Array.Empty<byte>();
            else
            {
                Span<byte> span = stackalloc byte[byteLength];
                stream.Read(span);
                bytes = span.ToArray();
            }

            return new Field(key, new Variant(type, bytes));
        }

        private static string ReadString(MemoryStream stream)
        {
            var byteLength = stream.Read<int>();

            if (byteLength == -1)
                return null;

            if (byteLength == 0)
                return string.Empty;

            Span<byte> bytes = stackalloc byte[byteLength];
            stream.Read(bytes);

            return Utf8Encoding.GetString(bytes);
        }

        private static void CheckTypeOrThrow(Type type, Variant variant)
        {
            if (type == typeof(sbyte) && variant.Type.HasFlag(VariantType.SByte))
                return;

            if (type == typeof(byte) && variant.Type.HasFlag(VariantType.Byte))
                return;

            if (type == typeof(short) && variant.Type.HasFlag(VariantType.Int16))
                return;

            if (type == typeof(ushort) && variant.Type.HasFlag(VariantType.UInt16))
                return;

            if (type == typeof(int) && variant.Type.HasFlag(VariantType.Int32))
                return;

            if (type == typeof(uint) && variant.Type.HasFlag(VariantType.UInt32))
                return;

            if (type == typeof(long) && variant.Type.HasFlag(VariantType.Int64))
                return;

            if (type == typeof(ulong) && variant.Type.HasFlag(VariantType.UInt64))
                return;



            if (type == typeof(string) && variant.Type == VariantType.String)
                return;


            var variantType = string.Join(", ", GetVariantTypeFlags(variant.Type));
            throw new InvalidOperationException($"Expected type is {type.Name} but variant has type of {variantType}.");
        }

        private static void CheckVariantIsArrayOrThrow(Variant variant)
        {
            if (variant.Type.HasFlag(VariantType.Array))
                return;

            throw new InvalidOperationException("Variant type is not array.");
        }

        private static void CheckVariantIsNotArrayOrThrow(Variant variant)
        {
            if (variant.Type.HasFlag(VariantType.Array) == false)
                return;

            throw new InvalidOperationException("Variant type is array.");
        }

        private static void CheckVariantIsNullableOrThrow(Variant variant)
        {
            if (variant.Type.HasFlag(VariantType.Nullable))
                return;

            throw new InvalidOperationException("Variant type is not Nullable.");
        }

        private static void CheckVariantIsNotNullableOrThrow(Variant variant)
        {
            if (variant.Type.HasFlag(VariantType.Nullable) == false)
                return;

            throw new InvalidOperationException("Variant type is Nullable.");
        }

        private static string[] GetVariantTypeFlags(VariantType value)
        {
            if (value == VariantType.None)
                return new [] { nameof(VariantType.None) };

            return ((VariantType[])Enum.GetValues(typeof(VariantType)))
                .Where(_ => _ != VariantType.None)
                .Where(_ => value.HasFlag(_))
                .Select(_ => Enum.GetName(typeof(VariantType), _))
                .ToArray();
        }
    }
}