using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IO;

// Thanks to https://github.com/jeanbern/jeanbern.github.io/blob/master/code/StreamExtensions.cs
namespace VariantObject
{
    public static class ByteConverter
    {
        private static readonly UTF8Encoding Utf8Encoding = new UTF8Encoding(false, true);
        private static readonly RecyclableMemoryStreamManager StreamManager = new RecyclableMemoryStreamManager();

        public static byte[] ValueToBytes<T>(T value)
            where T : unmanaged
        {
            using (var stream = StreamManager.GetStream())
            {
                var tSpan = MemoryMarshal.CreateSpan(ref value, 1);
                var span = MemoryMarshal.AsBytes(tSpan);
                stream.Write(span);
                return stream.ToArray();
            }
        }

        public static T BytesToValue<T>(byte[] value)
            where T : unmanaged
        {
            return BytesToValue<T>(value.AsSpan());
        }

        public static T BytesToValue<T>(ReadOnlySpan<byte> value)
            where T : unmanaged
        {
            using (var stream = StreamManager.GetStream())
            {
                stream.Write(value);
                stream.Position = 0;

                var result = default(T);
                var tSpan = MemoryMarshal.CreateSpan(ref result, 1);
                var span = MemoryMarshal.AsBytes(tSpan);
                stream.Read(span);
            
                return result;
            }
        }
        
        public static byte[] ArrayToBytes<T>(T[] values)
            where T : unmanaged
        {
            using (var stream = StreamManager.GetStream())
            {
                var tSpan = values.AsSpan();
                var span = MemoryMarshal.AsBytes(tSpan);
                stream.Write(values.Length);
                stream.Write(span);
                
                return stream.ToArray();
            }
        }

        public static T[] BytesToArray<T>(byte[] value)
            where T : unmanaged
        {
            return BytesToArray<T>(value.AsSpan());
        }

        public static T[] BytesToArray<T>(ReadOnlySpan<byte> value)
            where T : unmanaged
        {
            using (var stream = StreamManager.GetStream())
            {
                stream.Write(value);
                stream.Position = 0;

                var length = stream.Read<int>();
                var results = new T[length];
                var tSpan = results.AsSpan();
                var span = MemoryMarshal.AsBytes(tSpan);
                stream.Read(span);
            
                return results;
            }
        }

        public static byte[] StringToBytes(string value)
        {
            using (var stream = StreamManager.GetStream())
            {
                var valueSpan = value.AsSpan();
                var length = Utf8Encoding.GetByteCount(valueSpan);

                Span<byte> byteSpan = stackalloc byte[length];
                var encodedLength = Utf8Encoding.GetBytes(valueSpan, byteSpan);

                stream.Write(encodedLength);
                stream.Write(byteSpan);

                return stream.ToArray();
            }
        }

        public static string BytesToString(byte[] value)
        {
            return BytesToString(value.AsSpan());
        }

        public static string BytesToString(ReadOnlySpan<byte> value)
        {
            using (var stream = StreamManager.GetStream())
            {
                stream.Write(value);
                stream.Position = 0;

                var byteLength = stream.Read<int>();
                Span<byte> bytes = stackalloc byte[byteLength];
                stream.Read(bytes);

                return Utf8Encoding.GetString(bytes);
            }
        }

        public static byte[] StringsToBytes(string[] values)
        {
            using (var stream = StreamManager.GetStream())
            {
                stream.Write(values.Length);

                foreach(var value in values)
                {
                    var valueSpan = value.AsSpan();
                    var length = Utf8Encoding.GetByteCount(valueSpan);

                    Span<byte> byteSpan = stackalloc byte[length];
                    var encodedLength = Utf8Encoding.GetBytes(valueSpan, byteSpan);

                    stream.Write(encodedLength);
                    stream.Write(byteSpan);
                }

                return stream.ToArray();
            }
        }

        public static string[] BytesToStrings(byte[] value)
        {
            return BytesToStrings(value.AsSpan());
        }

        public static string[] BytesToStrings(ReadOnlySpan<byte> value)
        {
            using (var stream = StreamManager.GetStream())
            {
                stream.Write(value);
                stream.Position = 0;

                var arrayLength = stream.Read<int>();
                var result = new string[arrayLength];

                for(var i = 0; i < arrayLength; i++)
                {
                    var byteLength = stream.Read<int>();
                    Span<byte> bytes = stackalloc byte[byteLength];
                    stream.Read(bytes);
                    result[i] = Utf8Encoding.GetString(bytes);
                }

                return result;
            }
        }

        public static Guid BytesToMd5HashGuid(byte[] buffer)
        {
            using(var hashComputer = MD5.Create())
            {
                var hash = hashComputer.ComputeHash(buffer);
                return new Guid(hash);
            }
        }
    }
}