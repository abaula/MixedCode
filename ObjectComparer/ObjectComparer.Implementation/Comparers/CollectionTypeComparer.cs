using System.Collections.Generic;
using ObjectComparer.Abstractions.Comparers;
using ObjectComparer.Abstractions.Results;
using ObjectComparer.Implementation.Helpers;
using ObjectComparer.Implementation.Results;

namespace ObjectComparer.Implementation.Comparers
{
    public class CollectionTypeComparer<TMember, TTypeComparer>
        where TTypeComparer : ITypeComparer<TMember>, new()
        where TMember : class
    {
        private readonly TTypeComparer _typeComparer;

        public CollectionTypeComparer()
        {
            _typeComparer = new TTypeComparer();
        }

        public IEnumerable<ICollectionMemberCompareResult<TMember>> Compare(IEnumerable<TMember> left,
            IEnumerable<TMember> right)
        {
            var list = new List<ICollectionMemberCompareResult<TMember>>();

            using (var leftEnumerator = new SafeNullableEnumerator<TMember>(left))
            using (var rightEnumerator = new SafeNullableEnumerator<TMember>(right))
            {
                leftEnumerator.MoveNext();
                rightEnumerator.MoveNext();

                while (!leftEnumerator.Completed || !rightEnumerator.Completed)
                {
                    var typeComparerResult = _typeComparer.Compare(leftEnumerator.Current, rightEnumerator.Current);

                    list.Add(new CollectionTypeMemberCompareResult<TMember>
                    {
                        Match = typeComparerResult.Match,
                        Left = leftEnumerator.Current,
                        Right = rightEnumerator.Current,
                        TypeCompareResult = typeComparerResult
                    });

                    leftEnumerator.MoveNext();
                    rightEnumerator.MoveNext();
                }
            }

            return list;
        }
    }
}
