using System;

namespace Lucas.Solutions.Network
{
    public static class LogglyExtensions
    {
        public static string ToLogglyDateTime(this DateTime date)
        {
            return date.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.000Z");
        }
    }
}