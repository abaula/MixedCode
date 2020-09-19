using System;

namespace ObjectGenerator
{
    [Serializable]
    // TODO заменить Id на HashCode.
    public readonly struct DataObject
    {
        public static readonly DataObject Empty = new DataObject();
        public readonly Guid Id;
        public readonly string Type;
        public readonly DataObjectFieldDescriptor[] Fileds;
        public readonly byte[] Data;

        public DataObject(Guid id, string type, DataObjectFieldDescriptor[] fileds, byte[] data)
            => (Id, Type, Fileds, Data) = (id, type, fileds, data);
    }
}