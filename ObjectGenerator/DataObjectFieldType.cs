using System;

namespace ObjectGenerator
{
    [Serializable]
    // TODO Объединить с флагами, сделать единое перечисление.
    public enum DataObjectFieldType
    {
        DataObject,
        Guid,
        String,
        Int16,
        Int32,
        Int64,
        Single,
        Float,
        Double,
        Boolean,
        Byte,
        DateTime,
        TimeSpan
    }
}