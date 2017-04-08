namespace ObjectComparer.Abstractions.Results
{
    public interface ICollectionMemberCompareResult<out TObject>
    {
        bool Match { get; }
        TObject Left { get; }
        TObject Right { get; }
        ITypeCompareResult<TObject> TypeCompareResult { get; }
    }
}
