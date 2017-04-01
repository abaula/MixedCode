using System;
using System.Collections.Generic;

namespace ObjectComparer.Implementation.Comparers
{
    public class StringComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return string.Compare(x, y, StringComparison.CurrentCulture) == 0;
        }

        public int GetHashCode(string obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}
