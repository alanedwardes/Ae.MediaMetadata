using System;

namespace Ae.MediaMetadata.Entities
{
    public sealed class MediaInfo
    {
        public (int Width, int Height) Size { get; set; }
        public float? Duration { get; set; }
        public DateTimeOffset? CreationTime { get; set; }
        public string? CameraMake { get; set; }
        public string? CameraModel { get; set; }
        public string? CameraSoftware { get; set; }
        public (double Latitude, double Longitude)? Location { get; set; }
        public double? LocationAltitude { get; set; }
        public double? DigitalZoomRatio { get; set; }
        public double? ExposureIndex { get; set; }
        public TimeSpan? ShutterSpeedValue { get; set; }
        public double? BrightnessValue { get; set; }
        public double? ApertureValue { get; set; }
        public ushort? FocalLengthIn35mmFilm { get; set; }
        public double? FocalLength { get; set; }
        public double? ExposureBias { get; set; }
        public uint? IsoSpeed { get; internal set; }
        public TimeSpan? ExposureTime { get; internal set; }
        public double? FStop { get; internal set; }
        public MediaOrientation? Orientation { get; set; }
        public MediaFlash? Flash { get; set; }
        public MediaSaturation? Saturation { get; internal set; }
        public MediaExposureProgram? ExposureProgram { get; set; }
        public MediaWhiteBalance? WhiteBalance { get; set; }
        public MediaMeteringMode? MeteringMode { get; set; }
        public MediaContrast? Contrast { get; set; }
    }
}
