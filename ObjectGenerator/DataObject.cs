using System;

namespace ObjectGenerator
{
    [Serializable]
    public readonly struct DataObject
    {
        public static readonly DataObject Empty = new DataObject();
        public readonly Guid Id;
        public readonly DataObjectFieldDescriptor[] Fileds;
        public readonly byte[] Data;

        public DataObject(Guid id, DataObjectFieldDescriptor[] fileds, byte[] data)
            => (Id, Fileds, Data) = (id, fileds, data);
    }
}