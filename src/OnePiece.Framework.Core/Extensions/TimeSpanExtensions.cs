using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class TimeSpanExtensions
    {
        public static string Describe(this TimeSpan value)
        {
            var result = string.Empty;

            if (value.TotalSeconds < 60)
            {
                result = "刚刚";
            }
            else if (value.TotalSeconds >= 60 && value.TotalSeconds <= 3600)
            {
                result = string.Format("{0}分钟前", (int)Math.Round(value.TotalMinutes));
            }
            else if (value.TotalHours > 1 && value.TotalHours <= 24)
            {
                result = string.Format("{0}小时前", (int)Math.Round(value.TotalHours));
            }
            else if (value.TotalDays > 1 && value.TotalDays <= 30)
            {
                result = string.Format("{0}天前", (int)Math.Round(value.TotalDays));
            }
            else if (value.TotalDays > 30 && value.TotalDays <= 365)
            {
                result = string.Format("{0}个月前", (int)Math.Round(value.TotalDays / 30.4));
            }
            else
            {
                result = string.Format("{0}年前", (int)Math.Round(value.TotalDays / 365));
            }

            return result;
        }

        /// <summary>
        /// Before present
        /// 距今。。。。
        /// </summary>
        /// <param name="thatTime"></param>
        /// <returns></returns>
        public static string BeforePresent(this DateTime thatTime)
        {
            return (DateTime.Now - thatTime).Describe();
        }

        /// <summary>
        /// before some time...
        /// </summary>
        /// <param name="thatTime"></param>
        /// <param name="present"></param>
        /// <returns></returns>
        public static string BeforePresent(this DateTime thatTime, DateTime present)
        {
            return (present - thatTime).Describe();
        }
    }
}
