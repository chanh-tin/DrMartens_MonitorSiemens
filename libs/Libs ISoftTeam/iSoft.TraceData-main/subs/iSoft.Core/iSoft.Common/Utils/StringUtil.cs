using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static iSoft.Common.ConstCommon;

namespace iSoft.Common.Utils
{
    public static class StringUtil
    {

        public static string Join(this string[] arr, string joinSeperator)
        {
            return string.Join(joinSeperator, arr);
        }

        /// <summary>
        /// GenerateRandomKeyWithTicks
        /// </summary>
        /// <returns>EX: 638340036329629866_28286595</returns>
        public static string GenerateRandomKeyWithTicks()
        {
            return DateTime.Now.Ticks.ToString() + "_" + NumberUtil.GetRandomInt(10000000, 99999999).ToString();
        }

        /// <summary>
        /// GenerateRandomKeyWithDateTime
        /// </summary>
        /// <returns>EX: 20231027_150750_28286595</returns>
        public static string GenerateRandomKeyWithDateTime()
        {
            return DateTimeUtil.GetDateTimeStr(DateTime.Now, ConstDateTimeFormat.YYYYMMDD_HHMMSS_MIN) + "_" + NumberUtil.GetRandomInt(10000000, 99999999).ToString();
        }
        //public static Guid NewGUID()
        //{
        //    string randomStr = DateTimeUtil.GetDateTimeStr(DateTime.Now, ConstDateTimeFormat.YYYYMMDDTHHMMSS_FFF)
        //                        + NumberUtil.GetRandomInt(10000000, 99999999).ToString();


        //    var keyBytes = Encoding.UTF8.GetBytes(EncodeUtil.ConstPassEndcode);
        //    var passwordBytes = Encoding.UTF8.GetBytes(randomStr);

        //    var hmacMD5 = new HMACMD5(keyBytes);
        //    byte[] dataHash = hmacMD5.ComputeHash(passwordBytes);

        //    return new Guid(dataHash);
        //}
        public static Guid NewGUID()
        {
            byte[] ticksBytes = BitConverter.GetBytes(DateTime.Now.Ticks);
            byte[] randomBytes = new byte[8];
            new Random().NextBytes(randomBytes);

            byte[] combinedBytes = new byte[16];
            Array.Copy(randomBytes, combinedBytes, 4);
            Array.Copy(ticksBytes, 0, combinedBytes, 4, 8);
            Array.Copy(randomBytes, 4, combinedBytes, 12, 4);

            return new Guid(combinedBytes);
        }

        //public static Guid GenerateGuidFromTicks(long ticks)
        //{
        //    byte[] ticksBytes = BitConverter.GetBytes(ticks);
        //    byte[] randomBytes = new byte[10];
        //    new Random().NextBytes(randomBytes);

        //    byte[] combinedBytes = new byte[16];
        //    Array.Copy(ticksBytes, combinedBytes, 8);
        //    Array.Copy(randomBytes, 0, combinedBytes, 8, 8);

        //    return new Guid(combinedBytes);
        //}
        //public static Guid GenerateGuidFromTicks(DateTime date)
        //{
        //    byte[] ticksBytes = BitConverter.GetBytes(date.ToOADate());
        //    byte[] randomBytes = new byte[8];
        //    new Random().NextBytes(randomBytes);

        //    byte[] combinedBytes = new byte[16];
        //    Array.Copy(ticksBytes, combinedBytes, 8);
        //    Array.Copy(randomBytes, 0, combinedBytes, 8, 8);

        //    return new Guid(combinedBytes);
        //}

        /// <summary>
        /// GetStringInPattern
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern">@"\{\{(.+?)\}\}"</param>
        /// <returns></returns>
        public static string GetStringInPattern(this string str, string pattern)
        {
            //string pattern = @"\{\{(.+?)\}\}";
            Match match = Regex.Match(str, pattern);

            if (match.Success)
            {
                string extractedString = match.Groups[1].Value;
                return extractedString;
            }
            return "";
        }

        /// <summary>
        /// ReplaceTextInPattern
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern">@"\{.+?\}"</param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ReplaceTextInPattern(this string str, string pattern, string replacement)
        {
            return Regex.Replace(str, pattern, replacement);
        }

        public static bool IsMatch(string inputStr, string pattern)
        {
            return Regex.IsMatch(inputStr, pattern);
        }
        public static string Truncate(this string inputStr, int maxLength)
        {
            if (inputStr.Length <= maxLength)
            {
                return inputStr;
            }
            else
            {
                return inputStr.Substring(0, maxLength);
            }
        }
        public static (string fileName, string extension) SplitFileName(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);
            return (fileName, extension);
        }
        public static string GetFileExtension(this string filePath)
        {
            string extension = Path.GetExtension(filePath);
            return extension;
        }
        public static string GetDirectory(this string filePath)
        {
            string fileDirectory = Path.GetDirectoryName(filePath);
            return fileDirectory;
        }
        public static string RemoveUnicode(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }

        public static string RemoveSpecialChar(this string text, string escChar = @"\,")
        {
            string result = Regex.Replace(text, @"[^a-zA-Z0-9_" + escChar + @"]", "_");
            return result;
        }

        public static string SubstringSafe(this string text, int start, int length)
        {
            if (text.Length + start < length)
            {
                return text.Substring(start);
            }
            else if (text.Length < start)
            {
                return "";
            }
            else if (text.Length < start + length)
            {
                return text.Substring(start);
            }
            return text.Substring(start, length);
        }

        public static string ConvertToESField(this string text, string connectionId)
        {
            if (string.IsNullOrEmpty(connectionId))
            {
                return text.RemoveSpecialChar().Trim().ToLower();
            }
            return $"{connectionId}_{text}".RemoveSpecialChar().Trim().ToLower();
        }

        public static string GetESKey(this string text, string connectionId)
        {
            return $"{connectionId}_{text}".RemoveSpecialChar().Trim().ToLower();
        }

        public static string ToProper(this string str)
        {
            return str.UpperFirstLetter();
        }

        public static string PaddingLeft(this string text, int maxLen, char paddingCharacter = ' ')
        {
            if (text == null)
            {
                return new string(paddingCharacter, maxLen);
            }
            return text.PadLeft(maxLen, paddingCharacter);
        }

        public static string UpperFirstLetter(this string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string LowerFirstLetter(this string str)
        {
            return char.ToLower(str[0]) + str.Substring(1);
        }

        public static string RemoveLastChar(this string str)
        {
            if (str.Length >= 1)
            {
                return str.Substring(0, str.Length - 1);
            }
            return str;
        }

        public static double ToDoubleValue(this string text, double defaultValue)
        {
            double value;
            if (double.TryParse(text.Replace(',', '.').ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out value))
            {
                return value;
            }
            return defaultValue;
        }

        public static long ToLongValue(this string text, long defaultValue)
        {
            long value;
            if (long.TryParse(text, out value))
            {
                return value;
            }
            return defaultValue;
        }
        public static bool IsStrongPassword(this string password)
        {
            if (password.Length < ConstCommon.MINIMUM_LENGTH_PASSWORD || password.Length > ConstCommon.MAXIMUM_LENGTH_PASSWORD)
            {
                return false;
            }

            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(ch => !char.IsLetterOrDigit(ch));

            return hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }
    }

}
