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
            if (typeof(T) == typeof(VariantObject))
                throw new ArgumentException($"Use {nameof(ToVariant)}(VariantObject) method for VariantObject values.");

            var type = GetType(typeof(T));

            if (EqualityComparer<T>.Default.Equals(value, default))
                return new Variant(type, null);

            using var stream = MemoryStreamResource.GetStream();
            WriteValue(value, stream);

            return new Variant(type, stream.ToArray());
        }

        public static Variant ToVariant<T>(T? value)
            where T : unmanaged
        {
            if (typeof(T) == typeof(VariantObject))
                throw new ArgumentException($"Use {nameof(ToVariant)}(VariantObject) method for VariantObject values.");

            var type = GetType(typeof(T)) | VariantType.Nullable;

            if (value.HasValue == false)
                return new Variant(type, null);

            using var stream = MemoryStreamResource.GetStream();
            WriteValue(value.Value, stream);

            return new Variant(type, stream.ToArray());
        }

        private static void WriteValue<T>(T value, MemoryStream stream)
            where T : unmanaged
        {
            var tSpan = MemoryMarshal.CreateSpan(ref value, 1);
            var span = MemoryMarshal.AsBytes(tSpan);
            stream.Write(span);
        }

        public static Variant ToVariantArray<T>(T[] values)
            where T : unmanaged
        {
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

        public static Variant ToVariantArray<T>(T?[] values)
            where T : unmanaged
        {
            var type = GetType(typeof(T)) | VariantType.Nullable | VariantType.Array;

            if (values == null)
                return new Variant(type, null);

            using var stream = MemoryStreamResource.GetStream();
            stream.Write(values.Length);

            if (values.Length == 0)
                return new Variant(type, stream.ToArray());

            var valuesList = new List<T>();
            var nullPosList = new List<int>();

            for (var i = 0; i < values.Length; i++)
            {
                var value = values[i];

                if (value.HasValue == false)
                {
                    nullPosList.Add(i);
                    continue;
                }

                valuesList.Add(value.Value);
            }

            stream.Write(nullPosList.Count);

            if (nullPosList.Count > 0)
            {
                foreach (var nullPos in nullPosList)
                    stream.Write(nullPos);
            }

            stream.Write(valuesList.Count);

            if (valuesList.Count > 0)
            {
                foreach (var value in valuesList)
                    WriteValue(value, stream);
            }

            return new Variant(type, stream.ToArray());
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
                WriteField(field, stream);
            }
        }

        private static void WriteField(Field field, MemoryStream stream)
        {
            WriteString(field.Key, stream);
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
            var varType = VariantType.None;

            if (type == typeof(sbyte))
                varType = VariantType.SByte;

            if (type == typeof(byte))
                varType = VariantType.Byte;

            if (type == typeof(short))
                varType = VariantType.Int16;

            if (type == typeof(ushort))
                varType = VariantType.UInt16;

            if (type == typeof(int))
                varType = VariantType.Int32;

            if (type == typeof(uint))
                varType = VariantType.UInt32;

            if (type == typeof(long))
                varType = VariantType.Int64;

            if (type == typeof(ulong))
                varType = VariantType.UInt64;

            if (type == typeof(float))
                varType = VariantType.Float;

            if (type == typeof(double))
                varType = VariantType.Double;

            if (type == typeof(decimal))
                varType = VariantType.Decimal;

            if (type == typeof(bool))
                varType = VariantType.Bool;

            if (type == typeof(char))
                varType = VariantType.Char;

            if (type == typeof(string))
                varType = VariantType.String;

            if (type == typeof(DateTime))
                varType = VariantType.DateTime;

            if (type == typeof(TimeSpan))
                varType = VariantType.TimeSpan;

            if (type == typeof(Guid))
                varType = VariantType.Guid;

            return varType;
        }

        private static bool IsNullable(Type type)
        {
            if (type.IsValueType == false)
                return true;

            if (Nullable.GetUnderlyingType(type) != null)
                return true;

            return false;
        }
    }
}