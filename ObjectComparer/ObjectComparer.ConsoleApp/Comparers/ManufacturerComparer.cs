using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ObjectComparer.Abstractions.Results;
using ObjectComparer.ConsoleApp.Dtos;
using ObjectComparer.Implementation.Comparers;
using ObjectComparer.Implementation.Helpers;
using ObjectComparer.Implementation.Results;
using StringComparer = ObjectComparer.Implementation.Comparers.StringComparer;

namespace ObjectComparer.ConsoleApp.Comparers
{
    public class ManufacturerComparer : TypeComparerBase<ManufacturerDto>
    {
        private readonly StringComparer _stringComparer;
        private readonly NullableStructComparer<Guid> _guidComparer;
        private readonly CollectionTypeComparer<ContactDto, ContactComparer> _contactsTypeComparer;
        private readonly BothNullOrNotNullComparer _bothNullOrNotNullComparer;

        public ManufacturerComparer()
        {
            _stringComparer = new StringComparer();
            _guidComparer = new NullableStructComparer<Guid>();
            _contactsTypeComparer = new CollectionTypeComparer<ContactDto, ContactComparer>();
            _bothNullOrNotNullComparer = new BothNullOrNotNullComparer();
        }

        public override ITypeCompareResult<ManufacturerDto> Compare(ManufacturerDto left, 
            ManufacturerDto right,
            MemberInfo memberInfo = null)
        {
            if (left == null && right == null)
                return NullParamsTypeCompareResultBuilder.Build<ManufacturerDto>(memberInfo);

            var membersResults = new List<ICompareResult>();

            membersResults.Add(new MemberCompareResult<Guid?>
            {
                Left = left?.Id,
                Right = right?.Id,
                Member = Properties[nameof(ManufacturerDto.Id)],
                Match = _guidComparer.Equals(left?.Id, right?.Id)
            });

            membersResults.Add(new MemberCompareResult<string>
            {
                Left = left?.Name,
                Right = right?.Name,
                Member = Properties[nameof(ManufacturerDto.Name)],
                Match = _stringComparer.Equals(left?.Name, right?.Name)
            });

            var collectionResults = _contactsTypeComparer.Compare(left?.Contacts, right?.Contacts);
            var collectionBothNullOrNotNull = _bothNullOrNotNullComparer.Equals(left?.Contacts, right?.Contacts);

            membersResults.Add(new CollectionCompareResult<ContactDto>
            {
                Left = left?.Contacts,
                Right = right?.Contacts,
                Member = Properties[nameof(ManufacturerDto.Contacts)],
                CollectionResults = collectionResults,
                Match = (!collectionResults.Any() || collectionResults.All(cr => cr.Match)) && collectionBothNullOrNotNull
            });

            var bothNullOrNotNull = _bothNullOrNotNullComparer.Equals(left, right);

            return new TypeCompareResult<ManufacturerDto>
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
