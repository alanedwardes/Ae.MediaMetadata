// Coordinate.cs
// By Jaime Olivares
// Location: http://github.com/jaime-olivares/coordinate

using System;
using System.Globalization;

namespace Ae.MediaMetadata.Internal
{
    internal sealed class Coordinate
    {
        private double latitude;  // Expressed in seconds of degree, positive values for north
        private double longitude; // Expressed in seconds of degree, positive values for east

        public double Latitude => latitude / 3600.0d;

        public double Longitude => longitude / 3600.0d;

        public void SetDMS(double latDeg, double latMin, double latSec, bool north, double lonDeg, double lonMin, double lonSec, bool east)
        {
            latitude = (latDeg * 3600d + latMin * 60d + latSec) * (north ? 1d : -1d);
            longitude = (lonDeg * 3600d + lonMin * 60d + lonSec) * (east ? 1d : -1d);
        }

        public void SetDMS(double latDeg, double latMin, double latSec, string? latitudeRef, double lonDeg, double lonMin, double lonSec, string? longitudeRef)
        {
            SetDMS(latDeg, latMin, latSec, latitudeRef?.ToUpperInvariant() == "N", lonDeg, lonMin, lonSec, !(longitudeRef?.ToUpperInvariant() == "W"));
        }

        public void ParseIsoString(string isoStr)
        {
            if (isoStr.Length < 18)  // Check for minimum length
            {
                return;
            }

            if (!isoStr.EndsWith("/"))  // Check for trailing slash
            {
                return;
            }

            isoStr = isoStr.Remove(isoStr.Length - 1); // Remove trailing slash

            string[] parts = isoStr.Split(new char[] { '+', '-' }, StringSplitOptions.None);
            if (parts.Length < 3 || parts.Length > 4)  // Check for parts count
            {
                return;
            }

            if (parts[0].Length != 0)  // Check if first part is empty
            {
                return;
            }

            int point = parts[1].IndexOf('.');
            if (point != 2 && point != 4 && point != 6) // Check for valid length for lat/lon
            {
                return;
            }

            if (point != parts[2].IndexOf('.') - 1) // Check for lat/lon decimal positions
            {
                return;
            }

            NumberFormatInfo fi = NumberFormatInfo.InvariantInfo; 

            // Parse latitude and longitude values, according to format
            if (point == 2)
            {
                latitude = double.Parse(parts[1], fi) * 3600d;
                longitude = double.Parse(parts[2], fi) * 3600d;
            }
            else if (point == 4)
            {
                latitude = double.Parse(parts[1][..2], fi) * 3600d + double.Parse(parts[1][2..], fi) * 60d;
                longitude = double.Parse(parts[2][..3], fi) * 3600d + double.Parse(parts[2][3..], fi) * 60d;
            }
            else  // point==8
            {
                latitude = double.Parse(parts[1][..2], fi) * 3600d + double.Parse(parts[1].Substring(2, 2), fi) * 60d + double.Parse(parts[1][4..], fi);
                longitude = double.Parse(parts[2][..3], fi) * 3600d + double.Parse(parts[2].Substring(3, 2), fi) * 60d + double.Parse(parts[2][5..], fi);
            }
            // Parse altitude, just to check if it is valid
            if (parts.Length == 4)
            {
                double.Parse(parts[3], fi);
            }

            // Add proper sign to lat/lon
            if (isoStr[0] == '-')
            {
                latitude = -latitude;
            }

            if (isoStr[parts[1].Length + 1] == '-')
            {
                longitude = -longitude;
            }
        }
    }
}
