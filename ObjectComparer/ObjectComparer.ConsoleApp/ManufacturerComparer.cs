using System;
using System.Collections.Generic;
using System.Reflection;
using ObjectComparer.Abstractions.Results;
using ObjectComparer.Implementation;
using ObjectComparer.Implementation.Comparers;
using StringComparer = ObjectComparer.Implementation.Comparers.StringComparer;

namespace ObjectComparer.ConsoleApp
{
    public class ManufacturerComparer : TypeComparerBase<ManufacturerDto>
    {
        private readonly StringComparer _stringComparer;
        private readonly StructComparer<Guid> _guidComparer;

        public ManufacturerComparer()
        {
            _stringComparer = new StringComparer();
            _guidComparer = new StructComparer<Guid>();
        }

        public override ITypeCompareResult<ManufacturerDto> Compare(ManufacturerDto left, ManufacturerDto right, MemberInfo memberInfo = null)
        {
            var membersResults = new List<ICompareResult>();

            membersResults.Add(new MemberCompareResult<Guid>
            {
                Left = left.Id,
                Right = right.Id,
                Member = Properties[nameof(ManufacturerDto.Id)],
                Match = _guidComparer.Equals(left.Id, right.Id)
            });

            membersResults.Add(new MemberCompareResult<string>
            {
                Left = left.Name,
                Right = right.Name,
                Member = Properties[nameof(ManufacturerDto.Name)],
                Match = _stringComparer.Equals(left.Name, right.Name)
            });

            return new TypeCompareResult<ManufacturerDto>
            {
                Left = left,
                Right = right,
                Member = memberInfo,
                MembersResults = membersResults.ToArray()
            };
        }
    }
}
