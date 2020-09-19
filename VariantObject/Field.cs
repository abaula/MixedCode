using System;

namespace VariantObject
{
    [Serializable]
    public readonly struct Field : 
        IEquatable<Field>
    {
        public static readonly Field Empty = new Field();
        public readonly string Key;
        public readonly Variant Value;
        private readonly int _hashCode;
    
        public Field(string key, Variant value)
        {
            (Key, Value) = (key, value);
            _hashCode = HashCode.Combine(Key, Value);
        }

        public bool Equals(Field other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

        public static bool operator == (Field a, Field b) => a.GetHashCode() == b.GetHashCode();
        public static bool operator != (Field a, Field b) => !(a == b);
    }
}