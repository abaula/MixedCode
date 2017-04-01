using System.Reflection;

namespace ObjectComparer.Abstractions.Results
{
    public interface ICompareResult
    {
        bool Match { get; }
        MemberInfo Member { get; }
    }
}