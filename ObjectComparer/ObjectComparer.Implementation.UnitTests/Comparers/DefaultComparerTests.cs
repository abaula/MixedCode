using ObjectComparer.Implementation.Comparers;
using Xunit;

namespace ObjectComparer.Implementation.UnitTests.Comparers
{
    public class DefaultComparerTests
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
            var comparer = new DefaultComparer();
            var result = comparer.Equals(testValue1, testValue2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        [InlineData(null, true)]
        public void GetHashCode_Int_ZeroCheck(int? testValue, bool expected)
        {
            var comparer = new DefaultComparer();
            var hasCode = comparer.GetHashCode(testValue);
            var result = hasCode == 0;
            Assert.Equal(expected, result);
        }
    }
}
