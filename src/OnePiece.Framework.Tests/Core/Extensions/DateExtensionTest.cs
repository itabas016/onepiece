using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OnePiece.Framework.Tests.Core.Extensions
{
    public class DateExtensionTest
    {
        [Fact]
        public void should_return_integer_description()
        {
            var dt = DateTime.Now.AddMinutes(5);
            var origin = DateTime.Now;
            Thread.Sleep(10);
            Assert.Equal("刚刚", (DateTime.Now - origin).Describe());

            var span = dt - DateTime.Now;

            Assert.Equal("5分钟前", span.Describe());
            Assert.Equal("1小时前", span.Add(new TimeSpan(0, 60, 0)).Describe());
            Assert.Equal("1天前", span.Add(new TimeSpan(24, 2, 0)).Describe());
            Assert.Equal("1个月前", span.Add(new TimeSpan(31, 1, 1, 1)).Describe());
            Assert.Equal("1年前", span.Add(new TimeSpan(365, 2, 5, 8)).Describe());
        }

        [Fact]
        public void datetime_should_return_correct_integer()
        {
            Assert.Equal(10101, DateTime.MinValue.yyyyMMdd());
            Assert.Equal(20130723, new DateTime(2013, 7, 23).yyyyMMdd());
            Assert.Equal(99991231, DateTime.MaxValue.yyyyMMdd());
        }

        [Fact]
        public void weeknumber_start_from_monday()
        {
            Assert.Equal(0, new DateTime(1900, 1, 1).WeekNumber());
            Assert.Equal(0, new DateTime(1900, 1, 6).WeekNumber());
            Assert.Equal(0, new DateTime(1900, 1, 7).WeekNumber());
            Assert.Equal(1, new DateTime(1900, 1, 8).WeekNumber());
            Assert.Equal(2, new DateTime(1900, 1, 15).WeekNumber());
            Assert.Equal(5792, new DateTime(2011, 1, 3).WeekNumber());
            Assert.Equal(5896, new DateTime(2013, 1, 6).WeekNumber());
            Assert.Equal(5897, new DateTime(2013, 1, 7).WeekNumber());
        }

        [Fact]
        public void weeknumber_start_from_sunday()
        {
            Assert.Equal(0, new DateTime(1900, 1, 1).WeekNumber(DayOfWeek.Sunday));
            Assert.Equal(0, new DateTime(1900, 1, 6).WeekNumber(DayOfWeek.Sunday));
            Assert.Equal(1, new DateTime(1900, 1, 7).WeekNumber(DayOfWeek.Sunday));
            Assert.Equal(1, new DateTime(1900, 1, 8).WeekNumber(DayOfWeek.Sunday));
            Assert.Equal(2, new DateTime(1900, 1, 14).WeekNumber(DayOfWeek.Sunday));
            Assert.Equal(5792, new DateTime(2011, 1, 2).WeekNumber(DayOfWeek.Sunday));
            Assert.Equal(5897, new DateTime(2013, 1, 6).WeekNumber(DayOfWeek.Sunday));
        }

        [Fact(Skip ="GMT TIME ZONE")]
        public void datetime_convert_unix_stamp()
        {
            Assert.Equal(true, DateTime.Now.UnixStamp() > 0);
            Assert.Equal(1408871824, new DateTime(2014, 8, 24, 17, 17, 4).UnixStamp());
        }

        [Fact(Skip = "GMT TIME ZONE")]
        public void unix_stamp_convert_datetime()
        {
            Assert.Equal(true, ((long)1408871824).UTCStamp() == new DateTime(2014, 8, 24, 17, 17, 4));
            Assert.Equal(new DateTime(2014, 8, 24, 17, 17, 4), ((long)1408871824).UTCStamp());
        }
    }
}
