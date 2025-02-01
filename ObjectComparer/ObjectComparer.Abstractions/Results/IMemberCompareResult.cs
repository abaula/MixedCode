
namespace ObjectComparer.Abstractions.Results
{
    public interface IMemberCompareResult<out TObject> : ICompareResult
    {
        TObject Left { get; }
        TObject Right { get; }
    }
}
