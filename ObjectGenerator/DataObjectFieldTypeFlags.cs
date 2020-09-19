using System;

namespace ObjectGenerator
{
    [Flags]
    public enum DataObjectFieldTypeFlags
    {
        None = 0,
        Array = 1,
        Nullable = 2
    }
}