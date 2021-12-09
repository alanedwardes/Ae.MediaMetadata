// Coordinate.cs
// By Jaime Olivares
// Location: http://github.com/jaime-olivares/coordinate

using System;
using System.Globalization;

namespace Ae.MediaMetadata.Entities
{
    internal sealed class Coordinate
    {
        private double latitude;  // Expressed in seconds of degree, positive values for north
        private double longitude; // Expressed in seconds of degree, positive values for east

        public Coordinate()
        {
            Latitude = Longitude = 0.0f;
        }

        public Coordinate(double lat, double lon)  // Values expressed in degrees, for user convenience
        {
            Latitude = lat;
            Longitude = lon;
        }

        public double Latitude
        {
            set
            { 
                latitude = value * 3600.0f; 
            }
            get
            { 
                return latitude / 3600.0f;  // return degrees 
            }
        }

        public double Longitude
        {
            set
            {
                longitude = value * 3600.0f;
            }
            get
            {
                return longitude / 3600.0f;  // return degrees
            }
        }

        public void SetD(double latDeg, double lonDeg)
        {
            latitude = latDeg * 3600;  // Convert to seconds
            longitude = lonDeg * 3600; // Convert to seconds
        }

        public void SetDM(double latDeg, double latMin, bool north, double lonDeg, double lonMin, bool east)
        {
            latitude = (latDeg * 3600 + latMin * 60) * (north ? 1 : -1);
            longitude = (lonDeg * 3600 + lonMin * 60) * (east ? 1 : -1);
        }

        public void SetDMS(double latDeg, double latMin, double latSec, bool north, double lonDeg, double lonMin, double lonSec, bool east)
        {
            latitude = (latDeg * 3600 + latMin * 60 + latSec) * (north ? 1 : -1);
            longitude = (lonDeg * 3600 + lonMin * 60 + lonSec) * (east ? 1 : -1);
        }

        public void SetDMS(double latDeg, double latMin, double latSec, string? latitudeRef, double lonDeg, double lonMin, double lonSec, string? longitudeRef)
        {
            SetDMS(latDeg, latMin, latSec, latitudeRef?.ToUpperInvariant() == "N", lonDeg, lonMin, lonSec, !(longitudeRef?.ToUpperInvariant() == "W"));
        }

        public void GetD(out double latDeg, out double lonDeg)
        {
            latDeg = latitude / 3600.0f;
            lonDeg = longitude / 3600.0f;
        }

        public void GetDM(out double latDeg, out double latMin, out bool north, out double lonDeg, out double lonMin, out bool east)
        {
            north = latitude >= 0;
            double a = Math.Abs(latitude);
            latDeg = Math.Truncate(a / 3600.0);
            latMin = (a - latDeg * 3600.0) / 60.0f;

            east = longitude >= 0;
            double b = Math.Abs(longitude);
            lonDeg = Math.Truncate(b / 3600.0);
            lonMin = (b - lonDeg * 3600.0) / 60.0f;
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
                latitude = double.Parse(parts[1], fi) * 3600;
                longitude = double.Parse(parts[2], fi) * 3600;
            }
            else if (point == 4)
            {
                latitude = double.Parse(parts[1].Substring(0, 2), fi) * 3600 + double.Parse(parts[1].Substring(2), fi) * 60;
                longitude = double.Parse(parts[2].Substring(0, 3), fi) * 3600 + double.Parse(parts[2].Substring(3), fi) * 60;
            }
            else  // point==8
            {
                latitude = double.Parse(parts[1].Substring(0, 2), fi) * 3600 + double.Parse(parts[1].Substring(2, 2), fi) * 60 + double.Parse(parts[1].Substring(4), fi);
                longitude = double.Parse(parts[2].Substring(0, 3), fi) * 3600 + double.Parse(parts[2].Substring(3, 2), fi) * 60 + double.Parse(parts[2].Substring(5), fi);
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
