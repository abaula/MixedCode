using ObjectComparer.Abstractions;
using ObjectComparer.Abstractions.Comparers;

namespace ObjectComparer.Drafts
{
    public static class ObjectComparerFactoryExtension
    {
        public static void AddComparer<TObject>(this ObjectComparerFactory<TObject> factory, IObjectEqualityComparer comparer)
        {
            factory.Comparers.Add(comparer.Type, comparer);
        }
    }
}
