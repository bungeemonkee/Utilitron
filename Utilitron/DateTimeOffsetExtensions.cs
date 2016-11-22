using System;

namespace Utilitron
{
    /// <summary>
    /// General extension methods for <see cref="DateTimeOffset"/>s.
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Get the date component of this <see cref="DateTimeOffset"/> with the same offset.
        /// </summary>
        public static DateTimeOffset GetDate(this DateTimeOffset dto)
        {
            return dto.Subtract(dto.TimeOfDay);
        }
        /// <summary>
        /// Get the <see cref="DateTimeOffset"/> representing the last possible instant in the day.
        /// </summary>
        public static DateTimeOffset GetDateEnd(this DateTimeOffset dto)
        {
            return dto.AddDays(1).AddTicks(-1 - dto.TimeOfDay.Ticks);
        }

        /// <summary>
        /// Convert the given <see cref="DateTimeOffset"/> to the time zone specified by the given <see cref="TimeZoneInfo"/>.
        /// </summary>
        public static DateTimeOffset ToTimeZone(this DateTimeOffset dto, TimeZoneInfo tz)
        {
            return TimeZoneInfo.ConvertTime(dto, tz);
        }
    }
}
