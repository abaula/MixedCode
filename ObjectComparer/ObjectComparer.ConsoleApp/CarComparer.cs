using System;
using System.Collections.Generic;
using System.Reflection;
using ObjectComparer.Abstractions.Results;
using ObjectComparer.Implementation;
using ObjectComparer.Implementation.Comparers;
using StringComparer = ObjectComparer.Implementation.Comparers.StringComparer;

namespace ObjectComparer.ConsoleApp
{
    public class CarComparer : TypeComparerBase<CarDto>
    {
        private readonly StringComparer _stringComparer;
        private readonly StructComparer<Guid> _guidComparer;
        private readonly StructComparer<decimal> _decimalComparer;
        private readonly StructComparer<DateTime> _dateTimeComparer;
        private readonly ManufacturerComparer _manufacturerComparer;

        public CarComparer()
        {
            _stringComparer = new StringComparer();
            _guidComparer = new StructComparer<Guid>();
            _decimalComparer = new StructComparer<decimal>();
            _dateTimeComparer = new StructComparer<DateTime>();
            _manufacturerComparer = new ManufacturerComparer();
        }

        public override ITypeCompareResult<CarDto> Compare(CarDto left, CarDto right, MemberInfo memberInfo = null)
        {
            var membersResults = new List<ICompareResult>();

            membersResults.Add(new MemberCompareResult<Guid>
            {
                Left = left.Id,
                Right = right.Id,
                Member = Properties[nameof(CarDto.Id)],
                Match = _guidComparer.Equals(left.Id, right.Id)
            });

            membersResults.Add(new MemberCompareResult<DateTime>
            {
                Left = left.ManufactureDate,
                Right = right.ManufactureDate,
                Member = Properties[nameof(CarDto.ManufactureDate)],
                Match = _dateTimeComparer.Equals(left.ManufactureDate, right.ManufactureDate)
            });

            membersResults.Add(new MemberCompareResult<string>
            {
                Left = left.ModelName,
                Right = right.ModelName,
                Member = Properties[nameof(CarDto.ModelName)],
                Match = _stringComparer.Equals(left.ModelName, right.ModelName)
            });

            membersResults.Add(new MemberCompareResult<decimal>
            {
                Left = left.Price,
                Right = right.Price,
                Member = Properties[nameof(CarDto.Price)],
                Match = _decimalComparer.Equals(left.Price, right.Price)
            });

            membersResults.Add
                (
                _manufacturerComparer
                    .Compare(left.Manufacturer, 
                        right.Manufacturer, 
                        Properties[nameof(CarDto.Manufacturer)]
                        )
                );

            return new TypeCompareResult<CarDto>
            {
                Left = left,
                Right = right,
                Member = memberInfo,
                MembersResults = membersResults.ToArray()
            };
        }
    }
}
