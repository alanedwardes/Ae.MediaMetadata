using System.Text.Json.Serialization;

namespace Ae.MediaMetadata.Entities
{
    public sealed class MediaLocation
    {
        public MediaLocation()
        {
        }

        public MediaLocation(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }
        [JsonPropertyName("altitude")]
        public double? Altitude { get; set; }
    }
}
