using System.Text.Json.Serialization;

namespace Ae.MediaMetadata.Entities
{
    public sealed class MediaSize
    {
        public MediaSize()
        {
        }

        public MediaSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        [JsonPropertyName("width")]
        public int Width { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
    }
}
