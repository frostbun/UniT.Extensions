#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;

    public static class DateTimeExtensions
    {
        [Pure]
        public static DateTime GetFirstDayOfWeek(this DateTime dateTime, DayOfWeek firstDayOfWeek)
        {
            var diff           = dateTime.DayOfWeek - firstDayOfWeek;
            if (diff < 0) diff += 7;
            return dateTime.AddDays(-1 * diff).Date;
        }

        [Pure]
        public static DateTime GetFirstDayOfWeek(this DateTime dateTime, DateTimeFormatInfo dateTimeFormatInfo)
        {
            return dateTime.GetFirstDayOfWeek(dateTimeFormatInfo.FirstDayOfWeek);
        }

        [Pure]
        public static DateTime GetFirstDayOfWeek(this DateTime dateTime, CultureInfo cultureInfo)
        {
            return dateTime.GetFirstDayOfWeek(cultureInfo.DateTimeFormat);
        }

        [Pure]
        public static DateTime GetFirstDayOfWeek(this DateTime dateTime)
        {
            return dateTime.GetFirstDayOfWeek(CultureInfo.InvariantCulture);
        }

        [Pure]
        public static DateTime GetFirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        [Pure]
        public static DateTime GetFirstDayOfYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, 1, 1);
        }
    }
}