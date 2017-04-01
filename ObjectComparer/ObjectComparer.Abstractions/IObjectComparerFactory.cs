
using ObjectComparer.Abstractions.Comparers;

namespace ObjectComparer.Abstractions
{
    public interface IObjectComparerFactory<TObject>
    {
        ITypeComparer<TObject> Create();
    }
}
