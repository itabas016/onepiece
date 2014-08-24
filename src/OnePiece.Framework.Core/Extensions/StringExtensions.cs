using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace OnePiece.Framework.Core
{
    public static class StringExtensions
    {
        #region Consts
        const string TRUE = "true";
        const string FALSE = "false";
        public const string MatchEmailPattern =
           @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
    + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
    + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
    + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        const string CHINESE_PATTERN = @"[\u4e00-\u9fa5\uFF00-\uFFFF\s《》]*";

        const string CHINESE_SYMBOL = @"[\u0391-\uFFE5]";
        const string CHINESE_CHARACTER = @"[\\u4e00-\\u9fa5]";
        const string CHINESE_SYMBOL_AND_CHARACTER = @"[\u0391-\uFFE5]";

        private static Dictionary<string, string> CachedEncodedStringValue = new Dictionary<string, string>();
        #endregion

        #region Convertion
        public static Int32 ToInt32(this bool booleanValue)
        {
            return Convert.ToInt32(booleanValue);
        }

        public static Int32 ToInt32(this string integerStr)
        {
            return integerStr.ToInt32(0);
        }

        public static Int32 ToInt32(this string integerStr, Func<Int32> getDefaultValue)
        {
            return integerStr.ToInt32(getDefaultValue());
        }

        public static Int32 ToInt32(this string integerStr, Int32 defaultValue)
        {
            var value = 0;
            var canParse = int.TryParse(integerStr, out value);
            if (!canParse) value = defaultValue;

            return value;
        }

        public static UInt32 ToUInt32(this bool booleanValue)
        {
            return Convert.ToUInt32(booleanValue);
        }

        public static UInt32 ToUInt32(this string integerStr)
        {
            return integerStr.ToUInt32(0);
        }

        public static UInt32 ToUInt32(this string integerStr, Func<UInt32> getDefaultValue)
        {
            return integerStr.ToUInt32(getDefaultValue());
        }

        public static UInt32 ToUInt32(this string integerStr, UInt32 defaultValue)
        {
            var value = default(uint);
            var canParse = UInt32.TryParse(integerStr, out value);
            if (!canParse) value = defaultValue;

            return value;
        }

        public static Int64 ToInt64(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return 0;
            }

            var ret = default(long);
            long.TryParse(value, out ret);

            return ret;
        }

        public static long TruncLong(this string input)
        {
            long result = 0;
            if (!string.IsNullOrEmpty(input))
            {
                input = Regex.Replace(input, @"[^\d]", "");
                if (Regex.IsMatch(input, @"^[+-]?\d*$"))
                {
                    result = Int64.Parse(input);
                }
            }
            return result;
        }

        public static double ToDouble(this string doubleStr)
        {
            double val = 0.0;
            double.TryParse(doubleStr, out val);
            return val;
        }

        public static float ToFloat(this string floatStr)
        {
            float val = 0.0f;
            float.TryParse(floatStr, out val);
            return val;
        }

        public static Boolean ToBoolean(this string trueOrFalse)
        {
            var ret = false;

            if (!string.IsNullOrEmpty(trueOrFalse))
            {
                trueOrFalse = trueOrFalse.Trim();
                if (trueOrFalse == "0")
                    ret = false;
                else if (trueOrFalse == "1")
                    ret = true;
                else
                    ret = trueOrFalse.EqualsOrdinalIgnoreCase(TRUE);
            }

            return ret;
        }

        public static DateTime ToExactDateTime(this string dateTimeString, string format)
        {
            var ret = DateTime.MinValue;

            if (!dateTimeString.IsNullOrEmpty())
            {
                DateTime.TryParseExact(dateTimeString.Trim(), format, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out ret);
            }
            return ret;
        }
        #endregion

        #region Encoding

        /// <summary>
        /// This function will encode the special char with url encode strategy
        /// </summary>
        /// <param name="value"></param>
        /// <param name="special"></param>
        /// <returns></returns>
        public static string EncodeSpecial(this string value, string special)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var encoded = string.Empty;

                if (CachedEncodedStringValue.ContainsKey(special)) encoded = CachedEncodedStringValue[special];
                else
                {
                    encoded = HttpUtility.UrlEncode(special);
                    CachedEncodedStringValue[special] = encoded;
                }

                return value.Replace(special, encoded);
            }

            return value;
        }

        /// <summary>
        /// Will call HttpUtility.UrlEncode method
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UrlEncode(this string value)
        {
            return HttpUtility.UrlEncode(value);
        }

        /// <summary>
        /// 对URL中的中文进行编码
        /// 其中也包括空格
        /// 全角符号及《》
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncodeChineseChars(this string value)
        {
            // chinese characters & space chars


            var evaluator = new MatchEvaluator(match =>
            {
                var ret = match.Value.UrlEncode();
                return ret;
            });

            value = Regex.Replace(value, CHINESE_PATTERN, evaluator);

            return value;
        }

        /// <summary>
        /// Will call HttpUtility.UrlEncode method
        /// </summary>
        /// <param name="value"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string UrlEncode(this string value, Encoding encoding)
        {
            return HttpUtility.UrlEncode(value, encoding);
        }
        #endregion

        #region String Util
        /// <summary>
        /// wrapper IsNullOrEmpty
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string DefaultValue(this string str)
        {
            var value = str.IsNullOrEmpty() ? string.Empty : str;

            return value;
        }

        public static string FormatWith(this string pattern, params object[] args)
        {
            return string.Format(pattern, args);
        }

        /// <summary>
        /// Will not throw exception if the string is null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Lower(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }
            else
            {
                return value.ToLower();
            }
        }

        /// <summary>
        /// Will not throw exception if the string is null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Upper(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return string.Empty;
            }
            else
            {
                return value.ToUpper();
            }
        }

        /// <summary>
        /// equals to string.Equals(A, B);
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool EqualsOrdinalIgnoreCase(this string A, string B)
        {
            return string.Equals(A, B, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Make sure the string is not null
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string MakeSureNotNull(this string val)
        {
            if (val == null)
            {
                return string.Empty;
            }
            else
            {
                return val;
            }
        }

        public static bool IsEmail(this string emailStr)
        {
            return Regex.IsMatch(emailStr, MatchEmailPattern) && !Regex.IsMatch(emailStr, @"[\u4e00-\u9fa5\uFF00-\uFFFF]");
        }

        public static string ConfigValue(this string key)
        {
            return SingletonBase<ConfigurableSet>.Instance[key];
        }

        public static string TakeLength(this string value, int wantLength)
        {
            if (!value.IsNullOrEmpty())
            {
                if (value.Length >= wantLength && wantLength > 0) value = value.Substring(0, wantLength);

                if (wantLength < 0)
                {
                    var sb = new StringBuilder();
                    var l = Math.Abs(wantLength);
                    for (int i = value.Length - 1; i >= 0; i--)
                    {
                        sb.Append(value[i]);

                        if (sb.Length == l) break;
                    }

                    var array = sb.ToString().ToCharArray();
                    Array.Reverse(array);

                    return new string(array);
                }
            }

            return value;
        }

        /// <summary>
        /// UTF-8
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str)
        {
            if (str == null) return null;

            return Encoding.UTF8.GetBytes(str);
        }

        public static string SHA1Hash(this string str)
        {
            return Convert.ToBase64String(new SHA1CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(str.EncodeChineseChars())));
        }
        #endregion

        public static string Md5Hash(this string value)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// Make sure the path is a valid file path.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isLinuxFunc"></param>
        /// <returns></returns>
        public static string SafePath(this string path, Func<bool> isLinuxFunc = null)
        {
            if (path.IsNullOrEmpty()) return path;

            var isLinux = false;

            var ret = path;
            if (isLinuxFunc != null) { isLinux = isLinuxFunc(); }

            var pattern = string.Empty;
            var replacement = "\\";

            if (isLinux)
            {
                pattern = "/+";
                replacement = "/";
                ret = path.Replace("\\", replacement);
            }
            else
            {
                pattern = @"\\+";
                ret = path.Replace("/", replacement);
            }

            ret = ret.RegexReplace(pattern, replacement);

            return ret;
        }

        public static string RegexReplace(this string content, string pattern, string replacement)
        {
            var regex = new Regex(pattern);

            return regex.Replace(content, replacement);
        }

        public static bool VerifyMd5Hash(this string input, string hash)
        {
            string hashOfInput = Md5Hash(input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
