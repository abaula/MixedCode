using Xunit;
using StringComparer = ObjectComparer.Implementation.Comparers.StringComparer;

namespace ObjectComparer.Implementation.UnitTests.Comparers
{
    public class StringComparerTests
    {
        [Theory]
        [InlineData("abcd", "abcd", true)]
        [InlineData("", "", true)]
        [InlineData("ABCD", "ABCD", true)]
        [InlineData("abcd", "ABCD", false)]
        [InlineData("ABCD", "abcd", false)]
        [InlineData("abcd", "abc", false)]
        [InlineData("abcd", "abce", false)]
        [InlineData("abcd", "abcde", false)]
        [InlineData("abc", "abcd", false)]
        [InlineData("abce", "abcd", false)]
        [InlineData("abcde", "abcd", false)]
        [InlineData("abcd", "", false)]
        [InlineData("", "abcd", false)]
        [InlineData(null, null, true)]
        [InlineData("abcd", null, false)]
        [InlineData(null, "abcd", false)]
        public void Equals_Check(string testValue1, string testValue2, bool expected)
        {
            var comparer = new StringComparer();
            var result = comparer.Equals(testValue1, testValue2);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("abcd", false)]
        [InlineData(null, true)]
        public void GetHashCode_ZeroCheck(string testValue, bool expected)
        {
            var comparer = new StringComparer();
            var hasCode = comparer.GetHashCode(testValue);
            var result = hasCode == 0;
            Assert.Equal(expected, result);
        }
    }
}
