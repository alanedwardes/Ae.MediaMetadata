using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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

        public static DateTimeOffset? ParseGpsTime(string? date, IEnumerable<double> time)
        {
            var timeString = string.Join(':', time.Select(x => x.ToString().PadLeft(2, '0')));

            if (DateTimeOffset.TryParseExact(date + ' ' + timeString, "yyyy:MM:dd HH:mm:ss", null, DateTimeStyles.None, out var result))
            {
                return result;
            }

            return null;
        }
    }
}
