using System;

namespace Excalibur.Common.Extensions
{
    public static class LongExtensions
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromUnixTimeInMilliseconds(this long unixLong)
        {
            return Epoch.AddMilliseconds(unixLong);
        }

        public static DateTime FromUnixTimeInSeconds(this long unixLong)
        {
            return Epoch.AddSeconds(unixLong);
        }
    }
}