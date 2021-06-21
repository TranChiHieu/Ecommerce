using System;
using System.Collections.Generic;
using System.Text;

namespace Bitis.Library.Shared.Ultity
{
    public static class StringHelper
    {
        public static string EncodeBase64(this string value, Encoding encoding)
        {
            var bytes = encoding.GetBytes(value);
            var result = Convert.ToBase64String(bytes);
            return result;
        }

        public static string EncodeBase64(this string value)
        {
            return value.EncodeBase64(Encoding.UTF8);
        }

        public static string DecodeBase64(this string value, Encoding encoding)
        {
            var bytes = Convert.FromBase64String(value);
            var result = encoding.GetString(bytes);
            return result;
        }

        public static string DecodeBase64(this string value)
        {
            return value.DecodeBase64(Encoding.UTF8);
        }

        public static string ToCamelCase(this string str)
        {
            if (!string.IsNullOrEmpty(str) && str.Length > 1)
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }
            return str;
        }
    }
}
