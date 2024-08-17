#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Globalization;

    public static class DateTimeExtensions
    {
        public static DateTime GetFirstDayOfCurrentWeek(this DateTime dateTime, DayOfWeek firstDayOfWeek)
        {
            var diff           = dateTime.DayOfWeek - firstDayOfWeek;
            if (diff < 0) diff += 7;
            return dateTime.AddDays(-1 * diff).Date;
        }

        public static DateTime GetFirstDayOfCurrentWeek(this DateTime dateTime, CultureInfo cultureInfo)
        {
            return GetFirstDayOfCurrentWeek(dateTime, cultureInfo.DateTimeFormat.FirstDayOfWeek);
        }

        public static DateTime GetFirstDayOfCurrentWeek(this DateTime dateTime)
        {
            return GetFirstDayOfCurrentWeek(dateTime, CultureInfo.InvariantCulture);
        }

        public static DateTime GetFirstDayOfCurrentMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime GetFirstDayOfCurrentYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1);
        }
    }
}