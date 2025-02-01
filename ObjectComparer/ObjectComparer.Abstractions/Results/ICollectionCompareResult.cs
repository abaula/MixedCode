using System.Collections.Generic;

namespace ObjectComparer.Abstractions.Results
{
    public interface ICollectionCompareResult<out TItem> : IMemberCompareResult<IEnumerable<TItem>>
    {
        IEnumerable<ICollectionMemberCompareResult<TItem>> CollectionResults { get; }
        int MissmatchCount { get; }
        int MatchCount { get; }
    }
}
