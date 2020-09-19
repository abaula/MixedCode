using System;

namespace ObjectGenerator
{
    [Serializable]
    public enum DataObjectFieldType
    {
        DataObject,
        Null,
        Array,
        Guid,
        String,
        Number,
        Float,
        Boolean,
        Byte,
        DateTime,
        TimeSpan
    }
}