using System;
using System.Globalization;
using ObjectComparer.Implementation.Comparers;
using Xunit;
using StringComparer = ObjectComparer.Implementation.Comparers.StringComparer;

namespace ObjectComparer.Implementation.UnitTests.Comparers
{
    public class NullableStructComparerTests
    {
        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(0, 1, false)]
        [InlineData(1, 1, true)]
        [InlineData(null, null, true)]
        [InlineData(0, null, false)]
        [InlineData(null, 0, false)]
        public void Equals_Check(int? testValue1, int? testValue2, bool expected)
        {
            var comparer = new NullableStructComparer<int>();
            var result = comparer.Equals(testValue1, testValue2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        public void GetHashCode_Int_ZeroCheck(int? testValue, bool expected)
        {
            var comparer = new NullableStructComparer<int>();
            var hasCode = comparer.GetHashCode(testValue);
            var result = hasCode == 0;
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("0", true)]
        [InlineData("0.0", true)]
        [InlineData("0.2", false)]
        [InlineData("1.1", false)]
        public void GetHashCode_Decimal_ZeroCheck(string testValue, bool expected)
        {
            var comparer = new NullableStructComparer<decimal>();
            decimal? decimalTestValue = null;

            if (!string.IsNullOrEmpty(testValue))
                decimalTestValue = decimal.Parse(testValue, CultureInfo.InvariantCulture);

            var hasCode = comparer.GetHashCode(decimalTestValue);
            var result = hasCode == 0;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetHashCode_Null_Throws()
        {
            var comparer = new NullableStructComparer<int>();
            Assert.Throws<ArgumentNullException>(() => comparer.GetHashCode(null));
        }
    }
}
