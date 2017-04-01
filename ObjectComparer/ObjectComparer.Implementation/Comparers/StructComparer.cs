using System.Collections.Generic;

namespace ObjectComparer.Implementation.Comparers
{
    public class StructComparer<T> : IEqualityComparer<T>
        where T : struct
    {
        public bool Equals(T x, T y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }
}
