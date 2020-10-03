using System;

namespace VariantObject
{
    [Flags]
    [Serializable]
    public enum VariantType
    {
        None = 0,
        VariantObject = 1,
        Guid = 2,
        String = 4,
        SByte = 8,
        Byte = 16,
        Int16 = 32,
        UInt16 = 64,
        Int32 = 128,
        UInt32 = 256,
        Int64 = 512,
        UInt64 = 1024,

        Single = 2048,
        Double = 4096,
        Decimal = 8192,

        Boolean = 32768,
        Char = 65536,

        DateTime = 131072,
        TimeSpan = 262144,

        Array = 524288,
        Nullable = 1048576
    }
}