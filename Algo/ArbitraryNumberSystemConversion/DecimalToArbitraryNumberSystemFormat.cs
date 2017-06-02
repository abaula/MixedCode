using System;
using System.Globalization;

namespace ArbitraryNumberSystemConversion
{
    public class DecimalToArbitraryNumberSystemFormat : IFormatProvider, ICustomFormatter
    {
        private readonly string _numberSystem;

        public DecimalToArbitraryNumberSystemFormat(string numberSystem)
        {
            _numberSystem = numberSystem;
        }

        public string FormatKey => "ARB";

        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg.GetType() != typeof(long) && arg.GetType() != typeof(int))
            {
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException(string.Format("The format of '{0}' is invalid.", format), e);
                }
            }

            if (format?.ToUpper() != FormatKey)
            {
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException(string.Format("The format of '{0}' is invalid.", format), e);
                }
            }

            return DecimalToArbitrarySystem(Convert.ToInt64(arg));
        }

        /// <summary>
        /// Based on http://www.pvladov.com/2012/05/decimal-to-arbitrary-numeral-system.html
        /// </summary>
        private string DecimalToArbitrarySystem(long decimalNumber)
        {
            const int bitsInLong = 64;
            var radix = _numberSystem.Length;

            if (decimalNumber == 0)
                return _numberSystem[0].ToString();

            var index = bitsInLong - 1;
            var currentNumber = Math.Abs(decimalNumber);
            var charArray = new char[bitsInLong];

            while (currentNumber != 0)
            {
                var remainder = (int)(currentNumber % radix);
                charArray[index--] = _numberSystem[remainder];
                currentNumber = currentNumber / radix;
            }

            var result = new string(charArray, index + 1, bitsInLong - index - 1);

            if (decimalNumber < 0)
                result = "-" + result;

            return result;
        }

        private static string HandleOtherFormats(string format, object arg)
        {
            if (arg is IFormattable formattable)
                return formattable.ToString(format, CultureInfo.CurrentCulture);

            return arg != null ? arg.ToString() : string.Empty;
        }
    }
}
