using System;
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
            if (obj == null)
                throw new ArgumentNullException();

            return obj.GetHashCode();
        }
    }
}
