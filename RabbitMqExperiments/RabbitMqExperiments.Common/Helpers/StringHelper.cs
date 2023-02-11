using System;

namespace RabbitMqExperiments.Common.Helpers
{
    public static class StringHelper
    {
        public static bool Equals(string a, string b)
        {
            return string.Compare(a, b, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        public static bool NotNullOrEmpty(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool StartsWith(string source, string prefix)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            return source.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
