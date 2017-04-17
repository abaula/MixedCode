using System;
using System.Collections;

namespace ObjectComparer.Implementation.Comparers
{
    public class BothNullOrNotNullComparer : IEqualityComparer
    {
        public new bool Equals(object x, object y)
        {
            if (x == null && y == null)
                return true;

            if (x != null && y != null)
                return true;

            return false;
        }

        public int GetHashCode(object obj)
        {
            if(obj == null)
                throw new ArgumentNullException();

            return obj.GetHashCode();
        }
    }
}
