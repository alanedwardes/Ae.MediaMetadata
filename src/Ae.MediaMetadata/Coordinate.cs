// Coordinate.cs
// By Jaime Olivares
// Location: http://github.com/jaime-olivares/coordinate

using System;
using System.Globalization;

namespace Ae.MediaMetadata.Entities
{
    internal sealed class Coordinate
    {
        private float latitude;  // Expressed in seconds of degree, positive values for north
        private float longitude; // Expressed in seconds of degree, positive values for east

        public Coordinate()
        {
            Latitude = Longitude = 0.0f;
        }

        public Coordinate(float lat, float lon)  // Values expressed in degrees, for user convenience
        {
            Latitude = lat;
            Longitude = lon;
        }

        public float Latitude
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

        public float Longitude
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

        public void SetD(float latDeg, float lonDeg)
        {
            latitude = latDeg * 3600;  // Convert to seconds
            longitude = lonDeg * 3600; // Convert to seconds
        }

        public void SetDM(float latDeg, float latMin, bool north, float lonDeg, float lonMin, bool east)
        {
            latitude = (latDeg * 3600 + latMin * 60) * (north ? 1 : -1);
            longitude = (lonDeg * 3600 + lonMin * 60) * (east ? 1 : -1);
        }

        public void SetDMS(float latDeg, float latMin, float latSec, bool north, float lonDeg, float lonMin, float lonSec, bool east)
        {
            latitude = (latDeg * 3600 + latMin * 60 + latSec) * (north ? 1 : -1);
            longitude = (lonDeg * 3600 + lonMin * 60 + lonSec) * (east ? 1 : -1);
        }

        public void GetD(out float latDeg, out float lonDeg)
        {
            latDeg = latitude / 3600.0f;
            lonDeg = longitude / 3600.0f;
        }

        public void GetDM(out float latDeg, out float latMin, out bool north, out float lonDeg, out float lonMin, out bool east)
        {
            north = latitude >= 0;
            double a = Math.Abs(latitude);
            latDeg = (float)Math.Truncate(a / 3600.0);
            latMin = (float)(a - latDeg * 3600.0) / 60.0f;

            east = longitude >= 0;
            double b = Math.Abs(longitude);
            lonDeg = (float)Math.Truncate(b / 3600.0);
            lonMin = (float)(b - lonDeg * 3600.0) / 60.0f;
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
                latitude = float.Parse(parts[1], fi) * 3600;
                longitude = float.Parse(parts[2], fi) * 3600;
            }
            else if (point == 4)
            {
                latitude = float.Parse(parts[1].Substring(0, 2), fi) * 3600 + float.Parse(parts[1].Substring(2), fi) * 60;
                longitude = float.Parse(parts[2].Substring(0, 3), fi) * 3600 + float.Parse(parts[2].Substring(3), fi) * 60;
            }
            else  // point==8
            {
                latitude = float.Parse(parts[1].Substring(0, 2), fi) * 3600 + float.Parse(parts[1].Substring(2, 2), fi) * 60 + float.Parse(parts[1].Substring(4), fi);
                longitude = float.Parse(parts[2].Substring(0, 3), fi) * 3600 + float.Parse(parts[2].Substring(3, 2), fi) * 60 + float.Parse(parts[2].Substring(5), fi);
            }
            // Parse altitude, just to check if it is valid
            if (parts.Length == 4)
                float.Parse(parts[3], fi);

            // Add proper sign to lat/lon
            if (isoStr[0] == '-')
                latitude = -latitude;
            if (isoStr[parts[1].Length + 1] == '-')
                longitude = -longitude;
        }
    }
}
