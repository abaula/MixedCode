using System;

namespace VariantObject
{
    [Serializable]
    public readonly struct Variant : IEquatable<Variant>
    {
        public static readonly Variant Empty = new Variant();
        public readonly VariantType Type;
        public readonly byte[] Data;
        private readonly int _hashCode;

        public Variant(VariantType type, byte[] data)
        {
            (Type, Data) = (type, data);
            _hashCode = HashCode.Combine(Type, Data);
        }

        public bool Equals(Variant other)
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

        public static bool operator ==(Variant a, Variant b) => a.GetHashCode() == b.GetHashCode();
        public static bool operator !=(Variant a, Variant b) => !(a == b);
    }
}