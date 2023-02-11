using System.Collections.Generic;
using System.Linq;
using ObjectComparer.Abstractions.Results;

namespace ObjectComparer.Implementation.Results
{
    public class TypeCompareResult<TObject> : MemberCompareResult<TObject>, ITypeCompareResult<TObject>
    {
        public override bool Match { get; set; }
        public IEnumerable<ICompareResult> MembersResults { get; set; }
        public IEnumerable<ICompareResult> Missmatches => MembersResults.Where(mr => !mr.Match);
        public IEnumerable<ICompareResult> Matches => MembersResults.Where(mr => mr.Match);
    }
}
