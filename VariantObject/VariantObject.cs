using System;

namespace VariantObject
{
    [Serializable]
    public readonly struct VariantObject : IEquatable<VariantObject>
    {
        public static readonly VariantObject Empty = new VariantObject();
        public readonly string Type;
        public readonly Field[] Fields;
        private readonly int _hashCode;

        public VariantObject(string type, Field[] fields)
        {
            (Type, Fields) = (type, fields);
            _hashCode = HashCode.Combine(Type, Fields);
        }

        public bool Equals(VariantObject other)
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

        public static bool operator ==(VariantObject a, VariantObject b) => a.GetHashCode() == b.GetHashCode();
        public static bool operator !=(VariantObject a, VariantObject b) => !(a == b);
    }
}