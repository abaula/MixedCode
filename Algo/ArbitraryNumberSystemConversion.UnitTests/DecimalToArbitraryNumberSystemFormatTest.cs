using System;
using Xunit;

namespace ArbitraryNumberSystemConversion.UnitTests
{
    public class DecimalToArbitraryNumberSystemFormatTest
    {
        private const string NumberSystem = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        [Theory]
        [InlineData(0, "0", true)]
        [InlineData(35, "Z", true)]
        [InlineData(1295, "ZZ", true)]
        [InlineData(46655, "ZZZ", true)]
        [InlineData(1679580, "ZZZ0", true)]
        [InlineData(-1679580, "-ZZZ0", true)]
        [InlineData(-10, "-A", true)]
        public void Format_CheckFormat(long decimalNumberValue, string arbitraryNumberValue, bool expected)
        {
            var format = new DecimalToArbitraryNumberSystemFormat(NumberSystem);
            var formatString = string.Format("{{0:{0}}}", format.FormatKey);
            var result = string.Format(format, formatString, decimalNumberValue);
            Assert.Equal(expected, string.Compare(result, arbitraryNumberValue, StringComparison.CurrentCulture) == 0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(35)]
        [InlineData(1295)]
        [InlineData(46655)]
        [InlineData(1679580)]
        [InlineData(-1679580)]
        [InlineData(-10)]
        public void Format_SkipFormat_True(long decimalNumberValue)
        {
            var format = new DecimalToArbitraryNumberSystemFormat(NumberSystem);
            var result = string.Format(format, "{0}", decimalNumberValue);
            Assert.True(string.Compare(result, decimalNumberValue.ToString(), StringComparison.CurrentCulture) == 0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(35)]
        [InlineData(1295)]
        [InlineData(46655)]
        [InlineData(1679580)]
        [InlineData(-1679580)]
        [InlineData(-10)]
        public void Format_SkipFormat_NoConflict_True(long decimalNumberValue)
        {
            const string formatString = "{0:x}-{1}";
            var format = new DecimalToArbitraryNumberSystemFormat(NumberSystem);
            var result = string.Format(format, formatString, decimalNumberValue, decimalNumberValue + 1);
            Assert.True(string.Compare(result, string.Format(formatString, decimalNumberValue, decimalNumberValue + 1), StringComparison.CurrentCulture) == 0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(35)]
        [InlineData(1295)]
        [InlineData(46655)]
        [InlineData(1679580)]
        [InlineData(-1679580)]
        [InlineData(-10)]
        public void ParseToLong_InvalidFormat_Throws(long decimalNumberValue)
        {
            var format = new DecimalToArbitraryNumberSystemFormat(NumberSystem);
            Assert.Throws<FormatException>(() => string.Format(format, "{0:Z}", decimalNumberValue));
        }
    }
}
