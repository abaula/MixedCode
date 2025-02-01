using ObjectComparer.Abstractions.Results;

namespace ObjectComparer.Implementation.Results
{
    public class CollectionMemberCompareResult<TItem> : ICollectionMemberCompareResult<TItem>
    {
        public bool Match { get; set; }
        public TItem Left { get; set; }
        public TItem Right { get; set; }
        public ITypeCompareResult<TItem> TypeCompareResult => null;
    }
}
