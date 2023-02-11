using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectComparer.Abstractions.Results;

namespace ObjectComparer.Implementation.Results
{
    public class CollectionCompareResult<TItem> : ICollectionCompareResult<TItem>
    {
        public IEnumerable<ICollectionMemberCompareResult<TItem>> CollectionResults { get; set; }
        public int MissmatchCount => CollectionResults.Count(cr => !cr.Match);
        public int MatchCount => CollectionResults.Count(cr => cr.Match);
        public IEnumerable<TItem> Left { get; set; }
        public IEnumerable<TItem> Right { get; set; }
        public bool Match { get; set; }
        public MemberInfo Member { get; set; }
    }
}
