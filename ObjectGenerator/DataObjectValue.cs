using System;

namespace ObjectGenerator
{
    [Serializable]
    public readonly struct DataObjectValue
    {
        public static readonly DataObjectValue Empty = new DataObjectValue();
        public readonly DataObjectFieldDescriptor Filed;
        public readonly byte[] Data;

        public DataObjectValue(DataObjectFieldDescriptor filed, byte[] data)
            => (Filed, Data) = (filed, data);
    }
}