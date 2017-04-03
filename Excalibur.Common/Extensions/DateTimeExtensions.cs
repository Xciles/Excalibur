using System;

namespace Excalibur.Common.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTimeInMilliseconds(this DateTime time)
        {
            return (long)time.Subtract(Epoch).TotalMilliseconds;
        }

        public static long ToUnixTimeInSeconds(this DateTime time)
        {
            return (long)time.Subtract(Epoch).TotalMilliseconds;
        }
    }
}
