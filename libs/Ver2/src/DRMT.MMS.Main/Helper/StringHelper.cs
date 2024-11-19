using System.Text;
using System;

namespace DRMT.MMS.Main.Helper
{
    public static class StringHelper
    {
        public static string ConvertToBase64Str(string str)
        {
            var utf8Bytes = Encoding.UTF8.GetBytes(str);
            var base64String = Convert.ToBase64String(utf8Bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');
            return base64String;
        }

        public static string ConvertFromBase64Str(string base64Str)
        {
            var base64String = base64Str
                .Replace("-", "+")
                .Replace("_", "/");

            switch (base64String.Length % 4)
            {
                case 2: base64String += "=="; break;
                case 3: base64String += "="; break;
            }

            var bytes = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
