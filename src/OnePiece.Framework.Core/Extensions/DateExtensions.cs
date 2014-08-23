using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Core
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Get one integer for standing for that date...
        /// yyyyMMdd
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int yyyyMMdd(this DateTime datetime)
        {
            return datetime.ToString(DateTimeFormat.yyyyMMdd).ToInt32();
        }

        public static int yyyyMM(this DateTime datetime)
        {
            return datetime.ToString(DateTimeFormat.yyyyMM).ToInt32();
        }

        /// <summary>
        /// Represent the week number for one day.
        /// Considering the over-year date, we use the time span to calculate.
        /// The referecne date is 1900-1-1 (monday)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static int WeekNumber(this DateTime datetime, DayOfWeek firstDay = DayOfWeek.Monday)
        {
            var delta = ((int)firstDay) - 1;
            var referenceDate = new DateTime(1900, 1, 1);
            referenceDate = referenceDate.AddDays(delta);

            var span = datetime - referenceDate;
            var totalDays = (int)span.TotalDays;

            var number = totalDays / 7;

            return number;
        }
    }
}
