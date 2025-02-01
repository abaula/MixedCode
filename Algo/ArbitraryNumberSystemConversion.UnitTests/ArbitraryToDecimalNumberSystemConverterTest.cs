using System;
using Xunit;

namespace ArbitraryNumberSystemConversion.UnitTests
{
    public class ArbitraryToDecimalNumberSystemConverterTest
    {
        private const string NumberSystem = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        [Theory]
        [InlineData("0", 0, true)]
        [InlineData("Z", 35, true)]
        [InlineData("ZZ", 1295, true)]
        [InlineData("ZZZ", 46655, true)]
        [InlineData("ZZZ0", 1679580, true)]
        [InlineData("-ZZZ0", -1679580, true)]
        [InlineData("-0", 0, true)]
        public void ParseToLong_Check(string arbitraryNumberValue, long decimalNumberValue, bool expected)
        {
            var converter = new ArbitraryToDecimalNumberSystemConverter(NumberSystem);
            var result = converter.ToLong(arbitraryNumberValue);
            Assert.Equal(expected, result == decimalNumberValue);
        }

        [Theory]
        [InlineData("0-10")]
        [InlineData("0.1")]
        [InlineData("abc")]
        [InlineData("0aAB")]
        [InlineData("aAB")]
        public void ParseToLong_InvalidArgument_Throws(string arbitraryNumberValue)
        {
            var converter = new ArbitraryToDecimalNumberSystemConverter(NumberSystem);
            Assert.Throws<ArgumentException>(() => converter.ToLong(arbitraryNumberValue));
        }
    }
}
