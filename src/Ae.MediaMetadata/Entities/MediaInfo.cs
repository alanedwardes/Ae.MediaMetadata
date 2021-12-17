using System;
using System.Text.Json.Serialization;

namespace Ae.MediaMetadata.Entities
{
    public sealed class MediaInfo
    {
        [JsonPropertyName("size")]
        public MediaSize Size { get; set; } = new MediaSize();
        [JsonPropertyName("duration")]
        public float? Duration { get; set; }
        [JsonPropertyName("creationTime")]
        public DateTimeOffset? CreationTime { get; set; }
        [JsonPropertyName("cameraMake")]
        public string? CameraMake { get; set; }
        [JsonPropertyName("cameraModel")]
        public string? CameraModel { get; set; }
        [JsonPropertyName("cameraSoftware")]
        public string? CameraSoftware { get; set; }
        [JsonPropertyName("location")]
        public MediaLocation? Location { get; set; }
        [JsonPropertyName("digitalZoomRatio")]
        public double? DigitalZoomRatio { get; set; }
        [JsonPropertyName("exposureIndex")]
        public double? ExposureIndex { get; set; }
        [JsonPropertyName("shutterSpeedValue")]
        public double? ShutterSpeedValue { get; set; }
        [JsonPropertyName("brightnessValue")]
        public double? BrightnessValue { get; set; }
        [JsonPropertyName("apertureValue")]
        public double? ApertureValue { get; set; }
        [JsonPropertyName("focalLengthIn35mmFilm")]
        public ushort? FocalLengthIn35mmFilm { get; set; }
        [JsonPropertyName("focalLength")]
        public double? FocalLength { get; set; }
        [JsonPropertyName("exposureBias")]
        public double? ExposureBias { get; set; }
        [JsonPropertyName("isoSpeed")]
        public uint? IsoSpeed { get; set; }
        [JsonPropertyName("exposureTime")]
        public double? ExposureTime { get; set; }
        [JsonPropertyName("fStop")]
        public double? FStop { get; set; }
        [JsonPropertyName("orientation")]
        public MediaOrientation? Orientation { get; set; }
        [JsonPropertyName("flash")]
        public MediaFlash? Flash { get; set; }
        [JsonPropertyName("saturation")]
        public MediaSaturation? Saturation { get; set; }
        [JsonPropertyName("exposureProgram")]
        public MediaExposureProgram? ExposureProgram { get; set; }
        [JsonPropertyName("whiteBalance")]
        public MediaWhiteBalance? WhiteBalance { get; set; }
        [JsonPropertyName("meteringMode")]
        public MediaMeteringMode? MeteringMode { get; set; }
        [JsonPropertyName("contrast")]
        public MediaContrast? Contrast { get; set; }
        [JsonPropertyName("subjectDistanceRange")]
        public MediaSubjectDistanceRange? SubjectDistanceRange { get; set; }
        [JsonPropertyName("sceneCaptureType")]
        public MediaSceneCaptureType? SceneCaptureType { get; set; }
        [JsonPropertyName("sensingMethod")]
        public MediaSensingMethod? SensingMethod { get; set; }
        [JsonPropertyName("imageUniqueId")]
        public string? ImageUniqueId { get; set; }
    }
}
