using System;

namespace ObjectGenerator
{
    [Serializable]
    public readonly struct DataObjectFieldDescriptor : 
        IEquatable<DataObjectFieldDescriptor>, 
        IComparable<DataObjectFieldDescriptor>
    {
        public static readonly DataObjectFieldDescriptor Empty = new DataObjectFieldDescriptor();
        public readonly string Key;
        public readonly DataObjectFieldType Type;
        public readonly DataObjectFieldTypeFlags Flags;
        public readonly int Offset;
        public readonly int Length;

        public DataObjectFieldDescriptor(string key, DataObjectFieldType type, DataObjectFieldTypeFlags flags, int offset, int length)
            => (Key, Type, Flags, Offset, Length) = (key, type, flags, offset, length);

        public int CompareTo(DataObjectFieldDescriptor other)
        {
            return string.Compare(Key, other.Key);
        }

        public bool Equals(DataObjectFieldDescriptor other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key, Type, Flags, Offset, Length);
        }

        public static bool operator == (DataObjectFieldDescriptor a, DataObjectFieldDescriptor b) => (a.Key, a.Type, a.Flags, a.Length, a.Offset) == (b.Key, b.Type, b.Flags, b.Length, b.Offset);
        public static bool operator != (DataObjectFieldDescriptor a, DataObjectFieldDescriptor b) => !(a == b);
    }
}