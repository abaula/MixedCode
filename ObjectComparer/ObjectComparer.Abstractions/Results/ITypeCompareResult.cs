using System.Collections.Generic;

namespace ObjectComparer.Abstractions.Results
{
    public interface ITypeCompareResult<out T> : IMemberCompareResult<T>
    {
        IEnumerable<ICompareResult> MembersResults { get; }
        IEnumerable<ICompareResult> Missmatches { get; }
        IEnumerable<ICompareResult> Matches { get; }
    }
}
