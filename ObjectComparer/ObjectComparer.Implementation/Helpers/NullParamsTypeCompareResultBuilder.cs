using System;
using System.Reflection;
using ObjectComparer.Abstractions.Results;
using ObjectComparer.Implementation.Results;

namespace ObjectComparer.Implementation.Helpers
{
    public static class NullParamsTypeCompareResultBuilder
    {
        public static ITypeCompareResult<TObject> Build<TObject>(MemberInfo memberInfo = null)
            where TObject : class
        {
            return new TypeCompareResult<TObject>
            {
                Match = true,
                Left = null,
                Right = null,
                Member = memberInfo,
                MembersResults = Array.Empty<ICompareResult>()
            };
        }
    }
}
