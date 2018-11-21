using System;

namespace Excalibur.Avalon.Extensions
{
    /// <summary>
    /// This class contains a few useful extensions on Datetime />. 
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Returns the time in milliseconds from Epoch
        /// </summary>
        /// <param name="dt">The time to parse to milliseconds</param>
        /// <returns>The time in milliseconds since epoch</returns>
        public static long ToUnixTimeInMilliseconds(this DateTime dt) => (long)dt.Subtract(Epoch).TotalMilliseconds;

        /// <summary>
        /// Returns the time in seconds from Epoch
        /// </summary>
        /// <param name="dt">The time to parse to seconds</param>
        /// <returns>The time in seconds since epoch</returns>
        public static long ToUnixTimeInSeconds(this DateTime dt) => (long)dt.Subtract(Epoch).TotalSeconds;

        /// <summary>
        /// Returns the Datetime that was the start of the week for a given date and DayOfTheWeek
        /// </summary>
        /// <param name="dt">The time to use as input</param>
        /// <param name="startOfWeek">The day of the week that you want to use as a starting point</param>
        /// <returns>The time that was the start of the week</returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
