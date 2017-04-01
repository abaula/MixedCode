using System.Reflection;
using ObjectComparer.Abstractions.Results;

namespace ObjectComparer.Abstractions.Comparers
{
    public interface ITypeComparer<T>
    {
        ITypeCompareResult<T> Compare(T left, T right, MemberInfo memberInfo = null);
    }
}
