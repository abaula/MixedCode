using System;
using System.Collections.Generic;
using System.Reflection;
using ObjectComparer.Abstractions.Results;
using ObjectComparer.ConsoleApp.Dtos;
using ObjectComparer.ConsoleApp.Enums;
using ObjectComparer.Implementation.Comparers;
using ObjectComparer.Implementation.Helpers;
using ObjectComparer.Implementation.Results;
using StringComparer = ObjectComparer.Implementation.Comparers.StringComparer;

namespace ObjectComparer.ConsoleApp.Comparers
{
    public class ContactComparer : TypeComparerBase<ContactDto>
    {
        private readonly StringComparer _stringComparer;
        private readonly NullableStructComparer<Guid> _guidComparer;
        private readonly NullableStructComparer<int> _intComparer;
        private readonly NullableStructComparer<ContactType> _contactTypeComparer;
        private readonly BothNullOrNotNullComparer _bothNullOrNotNullComparer;

        public ContactComparer()
        {
            _stringComparer = new StringComparer();
            _guidComparer = new NullableStructComparer<Guid>();
            _contactTypeComparer = new NullableStructComparer<ContactType>();
            _intComparer = new NullableStructComparer<int>();
            _bothNullOrNotNullComparer = new BothNullOrNotNullComparer();
        }

        public override ITypeCompareResult<ContactDto> Compare(ContactDto left, ContactDto right,
            MemberInfo memberInfo = null)
        {
            if (left == null && right == null)
                return NullParamsTypeCompareResultBuilder.Build<ContactDto>(memberInfo);

            var membersResults = new List<ICompareResult>();

            membersResults.Add(new MemberCompareResult<Guid?>
            {
                Left = left?.Id,
                Right = right?.Id,
                Member = Properties[nameof(ContactDto.Id)],
                Match = _guidComparer.Equals(left?.Id, right?.Id)
            });

            membersResults.Add(new MemberCompareResult<ContactType?>
            {
                Left = left?.Type,
                Right = right?.Type,
                Member = Properties[nameof(ContactDto.Type)],
                Match = _contactTypeComparer.Equals(left?.Type, right?.Type)
            });

            membersResults.Add(new MemberCompareResult<int?>
            {
                Left = left?.Order,
                Right = right?.Order,
                Member = Properties[nameof(ContactDto.Order)],
                Match = _intComparer.Equals(left?.Order, right?.Order)
            });

            membersResults.Add(new MemberCompareResult<string>
            {
                Left = left?.Value,
                Right = right?.Value,
                Member = Properties[nameof(ContactDto.Value)],
                Match = _stringComparer.Equals(left?.Value, right?.Value)
            });

            membersResults.Add(new MemberCompareResult<string>
            {
                Left = left?.Description,
                Right = right?.Description,
                Member = Properties[nameof(ContactDto.Description)],
                Match = _stringComparer.Equals(left?.Description, right?.Description)
            });

            var bothNullOrNotNull = _bothNullOrNotNullComparer.Equals(left, right);

            return new TypeCompareResult<ContactDto>
            {
                Match = membersResults.TrueForAll(m => m.Match) && bothNullOrNotNull,
                Left = left,
                Right = right,
                Member = memberInfo,
                MembersResults = membersResults
            };
        }
    }
}
