using System;
using ObjectComparer.Implementation.Comparers;
using Xunit;

namespace ObjectComparer.Implementation.UnitTests.Comparers
{
    public class BothNullOrNotNullComparerTests
    {
        [Theory]
        [InlineData("testObject1", "testObject2", true)]
        [InlineData(null, null, true)]
        [InlineData("testObject1", null, false)]
        [InlineData(null, "testObject2", false)]
        public void Equals_Check(object testObject1, object testObject2, bool expected)
        {
            var comparer = new BothNullOrNotNullComparer();
            var result = comparer.Equals(testObject1, testObject2);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetHashCode_NotZero_ReturnsTrue()
        {
            var comparer = new BothNullOrNotNullComparer();
            var hasCode = comparer.GetHashCode(new object());
            Assert.NotEqual(0, hasCode);
        }

        [Fact]
        public void GetHashCode_Null_Throws()
        {
            var comparer = new BothNullOrNotNullComparer();
            Assert.Throws<ArgumentNullException>(() => comparer.GetHashCode(null));
        }
    }
}
