using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MultiMerge.UnitTests.Helpers
{
    static class TextMd5Helper
    {
        public static string GetMd5FromText(StringBuilder text)
        {
            byte[] buffer = new UTF8Encoding().GetBytes(text.ToString());
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(buffer);

            string encoded = BitConverter.ToString(hash)
               .Replace("-", string.Empty)
               .ToLower();

            return encoded;
        }
    }
}
