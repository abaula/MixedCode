using System;

namespace ObjectComparer.Abstractions.Comparers
{
    public interface IObjectEqualityComparer
    {
        Type Type { get; set; }
        bool Equals<T>(T left, T right);
    }
}
