using System;

namespace ArbitraryNumberSystemConversion
{
    public class ArbitraryToDecimalNumberSystemConverter
    {
        private readonly string _numberSystem;

        public ArbitraryToDecimalNumberSystemConverter(string numberSystem)
        {
            _numberSystem = numberSystem;
        }

        /// <summary>
        /// Based on http://www.pvladov.com/2012/07/arbitrary-to-decimal-numeral-system.html
        /// </summary>
        public long ToLong(string number)
        {
            var radix = _numberSystem.Length;

            if (string.IsNullOrEmpty(number))
                return 0;

            long result = 0;
            long multiplier = 1;

            for (var i = number.Length - 1; i >= 0; i--)
            {
                var c = number[i];

                if (i == 0 && c == '-')
                {
                    // This is the negative sign symbol
                    result = -result;
                    break;
                }

                var digit = _numberSystem.IndexOf(c);

                if (digit == -1)
                    throw new ArgumentException("Invalid character in the arbitrary number system", nameof(number));

                result += digit * multiplier;
                multiplier *= radix;
            }

            return result;
        }
    }
}
