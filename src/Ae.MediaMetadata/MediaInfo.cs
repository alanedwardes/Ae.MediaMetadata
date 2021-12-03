using System;

namespace Ae.Galeriya.Core.Entities
{
    public sealed class MediaInfo
    {
        public (int Width, int Height) Size { get; set; }
        public float? Duration { get; set; }
        public DateTimeOffset? CreationTime { get; set; }
        public string? CameraMake { get; set; }
        public string? CameraModel { get; set; }
        public string? CameraSoftware { get; set; }
        public (float Latitude, float Longitude)? Location { get; set; }
        public float? LocationAltitude { get; set; }
        public float? DigitalZoomRatio { get; set; }
        public float? ExposureIndex { get; set; }
        public TimeSpan? ShutterSpeedValue { get; set; }
        public float? BrightnessValue { get; set; }
        public float? ApertureValue { get; set; }
        public int? FocalLengthIn35mmFilm { get; set; }
        public float? FocalLength { get; set; }
        public float? ExposureBias { get; set; }
        public int? IsoSpeed { get; internal set; }
        public TimeSpan? ExposureTime { get; internal set; }
        public float? FStop { get; internal set; }
        public MediaOrientation? Orientation { get; set; }
        public MediaFlash? Flash { get; set; }
        public MediaSaturation? Saturation { get; internal set; }
        public MediaExposureProgram? ExposureProgram { get; set; }
        public MediaExposureProgram? WhiteBalance { get; set; }
        public MediaExposureProgram? MeteringMode { get; set; }
        public MediaExposureProgram? Contrast { get; set; }
    }
}
