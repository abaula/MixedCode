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
        Int16 = 8,
        Int32 = 16,
        Int64 = 32,
        Single = 64,
        Float = 128,
        Double = 256,
        Boolean = 512,
        Byte = 1024,
        DateTime = 2048,
        TimeSpan = 4096,
        Array = 8192,
        Nullable = 16384
    }
}