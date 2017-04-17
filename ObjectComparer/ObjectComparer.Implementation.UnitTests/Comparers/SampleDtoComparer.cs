using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using ObjectComparer.Abstractions.Results;
using ObjectComparer.Implementation.Comparers;
using ObjectComparer.Implementation.UnitTests.Dtos;

namespace ObjectComparer.Implementation.UnitTests.Comparers
{
    public class SampleDtoComparer : TypeComparerBase<SampleDto>
    {
        public new Dictionary<string, PropertyInfo> Properties => base.Properties;
        public new IEqualityComparer DefaultComparer => base.DefaultComparer;

        public override ITypeCompareResult<SampleDto> Compare(SampleDto left, SampleDto right, MemberInfo memberInfo = null)
        {
            throw new NotImplementedException();
        }
    }
}
