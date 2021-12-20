using System;
using System.Globalization;

namespace Ae.MediaMetadata.Internal
{
    internal static class MetadataStringExtensions
    {
        public static DateTimeOffset? ParseDateTimeString(this string? input)
        {
            if (DateTimeOffset.TryParse(input, null, DateTimeStyles.RoundtripKind, out var result))
            {
                return result;
            }

            if (DateTimeOffset.TryParseExact(input, "yyyy:MM:dd HH:mm:ss", null, DateTimeStyles.None, out result))
            {
                return result;
            }

            if (DateTimeOffset.TryParseExact(input, "yyyy:MM:dd h:mm:ss tt", null, DateTimeStyles.None, out result))
            {
                return result;
            }

            return null;
        }

        public static DateTimeOffset? ParseGpsTime(string? date, string? time)
        {
            if (DateTimeOffset.TryParseExact(date + ' ' + time, "yyyy:MM:dd HH:mm:ss", null, DateTimeStyles.None, out var result))
            {
                return result;
            }

            return null;
        }
    }
}
