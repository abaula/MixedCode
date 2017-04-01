using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectComparer.Abstractions.Comparers;
using ObjectComparer.Abstractions.Results;

namespace ObjectComparer.Implementation.Comparers
{
    public abstract class TypeComparerBase<TObject> : ITypeComparer<TObject>
    {
        protected readonly Dictionary<string, PropertyInfo> Properties;
        protected readonly IEqualityComparer DefaultComparer;

        protected TypeComparerBase()
        {
            Properties = typeof(TObject)
               .GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance)
               .ToDictionary(p => p.Name);

            DefaultComparer = new DefaultComparer();
        }

        public abstract ITypeCompareResult<TObject> Compare(TObject left, TObject right, MemberInfo memberInfo = null);
    }
}
