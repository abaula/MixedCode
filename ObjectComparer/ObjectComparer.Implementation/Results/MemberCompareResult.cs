using System.Reflection;
using ObjectComparer.Abstractions.Results;

namespace ObjectComparer.Implementation.Results
{
    public class MemberCompareResult<TMember> : IMemberCompareResult<TMember>
    {
        public virtual bool Match { get; set; }
        public TMember Left { get; set; }
        public TMember Right { get; set; }
        public MemberInfo Member { get; set; }
    }
}
