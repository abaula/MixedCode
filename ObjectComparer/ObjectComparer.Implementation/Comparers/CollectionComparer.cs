using System.Collections.Generic;
using ObjectComparer.Abstractions.Results;
using ObjectComparer.Implementation.Helpers;
using ObjectComparer.Implementation.Results;

namespace ObjectComparer.Implementation.Comparers
{
    public class CollectionComparer<TMember, TComparer>
        where TComparer : IEqualityComparer<TMember>, new()
    {
        private readonly TComparer _comparer;

        public CollectionComparer()
        {
            _comparer = new TComparer();
        }

        public ICollection<ICollectionMemberCompareResult<TMember>> Compare(IEnumerable<TMember> left,
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
                    var match = _comparer.Equals(leftEnumerator.Current, rightEnumerator.Current);

                    list.Add(new CollectionMemberCompareResult<TMember>
                    {
                        Match = match,
                        Left = leftEnumerator.Current,
                        Right = rightEnumerator.Current 

                    });

                    leftEnumerator.MoveNext();
                    rightEnumerator.MoveNext();
                }
            }

            return list;
        }
    }
}
