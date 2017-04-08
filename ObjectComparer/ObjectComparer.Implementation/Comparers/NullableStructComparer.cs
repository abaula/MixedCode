using System.Collections.Generic;

namespace ObjectComparer.Implementation.Comparers
{
    public class NullableStructComparer<T> : IEqualityComparer<T?>
        where T : struct
    {
        public bool Equals(T? x, T? y) => EqualityComparer<T?>.Default.Equals(x, y);
        public int GetHashCode(T? obj) => obj?.GetHashCode() ?? 0;
    }
}