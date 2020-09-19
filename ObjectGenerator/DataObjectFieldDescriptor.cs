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
        public readonly DataObjectFieldType FieldType;
        public readonly int Offset;
        public readonly int Length;

        public DataObjectFieldDescriptor(string key, DataObjectFieldType fieldType, int offset, int length)
            => (Key, FieldType, Offset, Length) = (key, fieldType, offset, length);

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
            return HashCode.Combine(Key, FieldType, Offset, Length);
        }

        public static bool operator == (DataObjectFieldDescriptor a, DataObjectFieldDescriptor b) => (a.Key, a.FieldType, a.Length, a.Offset) == (b.Key, b.FieldType, b.Length, b.Offset);
        public static bool operator != (DataObjectFieldDescriptor a, DataObjectFieldDescriptor b) => !(a == b);
    }
}