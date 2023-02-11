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
    public class CarComparer : TypeComparerBase<CarDto>
    {
        private readonly StringComparer _stringComparer;
        private readonly NullableStructComparer<Guid> _guidComparer;
        private readonly NullableStructComparer<decimal> _decimalComparer;
        private readonly NullableStructComparer<DateTime> _dateTimeComparer;
        private readonly ManufacturerComparer _manufacturerComparer;
        private readonly BothNullOrNotNullComparer _bothNullOrNotNullComparer;
        private readonly CollectionComparer<string, StringComparer> _codesComparer;

        public CarComparer()
        {
            _stringComparer = new StringComparer();
            _guidComparer = new NullableStructComparer<Guid>();
            _decimalComparer = new NullableStructComparer<decimal>();
            _dateTimeComparer = new NullableStructComparer<DateTime>();
            _manufacturerComparer = new ManufacturerComparer();
            _bothNullOrNotNullComparer = new BothNullOrNotNullComparer();
            _codesComparer = new CollectionComparer<string, StringComparer>();
        }

        public override ITypeCompareResult<CarDto> Compare(CarDto left, CarDto right, MemberInfo memberInfo = null)
        {
            if (left == null && right == null)
                return NullParamsTypeCompareResultBuilder.Build<CarDto>(memberInfo);

            var membersResults = new List<ICompareResult>();

            membersResults.Add(new MemberCompareResult<Guid?>
            {
                Left = left?.Id,
                Right = right?.Id,
                Member = Properties[nameof(CarDto.Id)],
                Match = _guidComparer.Equals(left?.Id, right?.Id)
            });

            membersResults.Add(new MemberCompareResult<DateTime?>
            {
                Left = left?.ManufactureDate,
                Right = right?.ManufactureDate,
                Member = Properties[nameof(CarDto.ManufactureDate)],
                Match = _dateTimeComparer.Equals(left?.ManufactureDate, right?.ManufactureDate)
            });

            membersResults.Add(new MemberCompareResult<string>
            {
                Left = left?.ModelName,
                Right = right?.ModelName,
                Member = Properties[nameof(CarDto.ModelName)],
                Match = _stringComparer.Equals(left?.ModelName, right?.ModelName)
            });

            membersResults.Add(new MemberCompareResult<decimal?>
            {
                Left = left?.Price,
                Right = right?.Price,
                Member = Properties[nameof(CarDto.Price)],
                Match = _decimalComparer.Equals(left?.Price, right?.Price)
            });

            membersResults.Add
            (
                _manufacturerComparer
                    .Compare(left?.Manufacturer,
                        right?.Manufacturer,
                        Properties[nameof(CarDto.Manufacturer)]
                    )
            );

            var collectionResults = _codesComparer.Compare(left?.WheelCodes, right?.WheelCodes);
            var collectionBothNullOrNotNull = _bothNullOrNotNullComparer.Equals(left?.WheelCodes, right?.WheelCodes);

            membersResults.Add(new CollectionCompareResult<string>
            {
                Left = left?.WheelCodes,
                Right = right?.WheelCodes,
                Member = Properties[nameof(CarDto.WheelCodes)],
                CollectionResults = collectionResults,
                Match = (!collectionResults.Any() || collectionResults.All(cr => cr.Match)) && collectionBothNullOrNotNull
            });

            var bothNullOrNotNull = _bothNullOrNotNullComparer.Equals(left, right);

            return new TypeCompareResult<CarDto>
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
