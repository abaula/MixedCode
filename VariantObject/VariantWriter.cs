using System;
using System.Collections.Generic;
using System.IO;
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

            if (typeof(T) == typeof(VariantObject))
                throw new ArgumentException($"Use {nameof(ToVariant)}(VariantObject) method for VariantObject values.");

            if (EqualityComparer<T>.Default.Equals(value, default))
                return new Variant(GetType(typeof(T)), null);

            using var stream = MemoryStreamResource.GetStream();
            var tSpan = MemoryMarshal.CreateSpan(ref value, 1);
            var span = MemoryMarshal.AsBytes(tSpan);
            stream.Write(span);

            return new Variant(GetType(typeof(T)), stream.ToArray());
        }

        public static Variant ToVariantArray<T>(T[] values)
            where T : unmanaged
        {
            if (typeof(T) == typeof(string))
                throw new ArgumentException($"Use overload {nameof(ToVariantArray)}(string[]) method for string values.");

            if (typeof(T) == typeof(char))
                throw new ArgumentException($"Use overload {nameof(ToVariantArray)}(char[]) method for char values.");

            if (typeof(T) == typeof(VariantObject))
                throw new ArgumentException($"Use {nameof(ToVariantArray)}(VariantObject[]) method for VariantObject values.");

            if (values == null)
                return new Variant(GetType(typeof(T)) | VariantType.Array, null);

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(values.Length);

            if (values.Length > 0)
            {
                var tSpan = values.AsSpan();
                var span = MemoryMarshal.AsBytes(tSpan);
                stream.Write(span);
            }

            return new Variant(GetType(typeof(T)) | VariantType.Array, stream.ToArray());
        }

        public static Variant ToVariant(string value)
        {
            using var stream = MemoryStreamResource.GetStream();
            WriteString(value, stream);

            return new Variant(VariantType.String, stream.ToArray());
        }

        public static Variant ToVariantArray(string[] values)
        {
            if (values == null)
                return new Variant(VariantType.String | VariantType.Array, null);

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(values.Length);

            if (values.Length > 0)
            {
                foreach (var value in values)
                    WriteString(value, stream);
            }

            return new Variant(VariantType.String | VariantType.Array, stream.ToArray());
        }

        public static Variant ToVariantArray(char[] values)
        {
            if (values == null)
                return new Variant(VariantType.Char | VariantType.Array, null);

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(values.Length);

            if (values.Length > 0)
            {
                var valueSpan = values.AsSpan();
                var encodedLength = Utf8Encoding.GetByteCount(valueSpan);

                Span<byte> byteSpan = stackalloc byte[encodedLength];
                var byteLength = Utf8Encoding.GetBytes(valueSpan, byteSpan);

                stream.Write(byteLength);
                stream.Write(byteSpan);
            }

            return new Variant(VariantType.Char | VariantType.Array, stream.ToArray());
        }

        public static Variant ToVariant(VariantObject value)
        {
            using var stream = MemoryStreamResource.GetStream();
            WriteVariant(value, stream);

            return new Variant(VariantType.VariantObject, stream.ToArray());
        }

        public static Variant ToVariantArray(VariantObject[] values)
        {
            if (values == null)
                return new Variant(VariantType.VariantObject | VariantType.Array, null);

            using var stream = MemoryStreamResource.GetStream();

            stream.Write(values.Length);

            if (values.Length > 0)
            {
                foreach (var value in values)
                    WriteVariant(value, stream);
            }

            return new Variant(VariantType.VariantObject | VariantType.Array, stream.ToArray());
        }

        private static void WriteVariant(VariantObject value, MemoryStream stream)
        {
            WriteString(value.Type, stream);

            if (value.Fields == null)
            {
                stream.Write(-1);
                return;
            }

            if (value.Fields.Length == 0)
            {
                stream.Write(0);
                return;
            }

            stream.Write(value.Fields.Length);

            foreach (var field in value.Fields)
            {
                WriteString(field.Key, stream);
                WriteField(field, stream);
            }
        }

        private static void WriteField(Field field, MemoryStream stream)
        {
            stream.Write(field.Value.Type);

            if (field.Value.Data == null)
            {
                stream.Write(-1);
                return;
            }

            if (field.Value.Data.Length == 0)
            {
                stream.Write(0);
                return;
            }

            stream.Write(field.Value.Data.Length);
            stream.Write(field.Value.Data);
        }

        private static void WriteString(string value, MemoryStream stream)
        {
            if (value == null)
            {
                stream.Write(-1);
                return;
            }

            if (value.Length == 0)
            {
                stream.Write(0);
                return;
            }

            var valueSpan = value.AsSpan();
            var length = Utf8Encoding.GetByteCount(valueSpan);

            Span<byte> byteSpan = stackalloc byte[length];
            var encodedLength = Utf8Encoding.GetBytes(valueSpan, byteSpan);

            stream.Write(encodedLength);
            stream.Write(byteSpan);
        }

        private static VariantType GetType(Type type)
        {
            return VariantType.None;
        }
    }
}