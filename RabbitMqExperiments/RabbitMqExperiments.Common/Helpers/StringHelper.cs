using System;

namespace RabbitMqExperiments.Common.Helpers
{
    public static class StringHelper
    {
        public static bool Equals(string a, string b)
        {
            return string.Compare(a, b, StringComparison.InvariantCultureIgnoreCase) == 0;
        }
    }
}
