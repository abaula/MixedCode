using System.Collections;

namespace ObjectComparer.Implementation.Comparers
{
    public class DefaultComparer : IEqualityComparer
    {
        public new bool Equals(object x, object y)
        {
            return object.Equals(x, y);
        }

        public int GetHashCode(object obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}
