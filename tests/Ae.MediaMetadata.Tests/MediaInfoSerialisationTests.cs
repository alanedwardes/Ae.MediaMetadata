using Ae.MediaMetadata.Entities;
using System;
using System.Text.Json;
using Xunit;

namespace Ae.MediaMetadata.Tests
{
    public sealed class MediaInfoSerialisationTests
    {
        [Fact]
        public void TestRoundTrip()
        {
            var expected = new MediaInfo
            {
                Size = new MediaSize(640, 480),
                ApertureValue = 1.4,
                BrightnessValue = 1,
                CameraMake = "wibble",
                CameraModel = "bibble",
                CameraSoftware = "bobble",
                Contrast = MediaContrast.Hard,
                DigitalZoomRatio = 1.3,
                CreationTime = new DateTimeOffset(634292263478068039, TimeSpan.Zero),
                Duration = 1.2f,
                ExposureBias = 4.5,
                ExposureIndex = 2.7,
                ExposureProgram = MediaExposureProgram.AperturePriority,
                ExposureTime = 0.1,
                Flash = MediaFlash.FlashDidNotFire,
                FocalLength = 2.4,
                FocalLengthIn35mmFilm = 25,
                FStop = 2.5,
                IsoSpeed = 70,
                Location = new(2, 3) { Altitude = 20 },
                MeteringMode = MediaMeteringMode.Average,
                Orientation = MediaOrientation.LeftTop,
                Saturation = MediaSaturation.LowSaturation,
                ShutterSpeedValue = 0.2,
                WhiteBalance = MediaWhiteBalance.Manual,
                SceneCaptureType = MediaSceneCaptureType.NightScene,
                ImageUniqueId = "wibble",
                SensingMethod = MediaSensingMethod.OneChipColorAreaSensor,
                SubjectDistanceRange = MediaSubjectDistanceRange.Macro
            };


            var json = JsonSerializer.Serialize(expected);

            var actual = JsonSerializer.Deserialize<MediaInfo>(json);

            Assert.Equal(expected.ApertureValue, actual.ApertureValue);
            Assert.Equal(expected.BrightnessValue, actual.BrightnessValue);
            Assert.Equal(expected.CameraMake, actual.CameraMake);
            Assert.Equal(expected.CameraModel, actual.CameraModel);
            Assert.Equal(expected.Contrast, actual.Contrast);
            Assert.Equal(expected.CreationTime, actual.CreationTime);
            Assert.Equal(expected.DigitalZoomRatio, actual.DigitalZoomRatio);
            Assert.Equal(expected.Duration, actual.Duration);
            Assert.Equal(expected.ExposureBias, actual.ExposureBias);
            Assert.Equal(expected.ExposureIndex, actual.ExposureIndex);
            Assert.Equal(expected.ExposureProgram, actual.ExposureProgram);
            Assert.Equal(expected.ExposureTime, actual.ExposureTime);
            Assert.Equal(expected.Flash, actual.Flash);
            Assert.Equal(expected.FocalLength, actual.FocalLength);
            Assert.Equal(expected.FocalLengthIn35mmFilm, actual.FocalLengthIn35mmFilm);
            Assert.Equal(expected.FStop, actual.FStop);
            Assert.Equal(expected.ImageUniqueId, actual.ImageUniqueId);
            Assert.Equal(expected.IsoSpeed, actual.IsoSpeed);
            Assert.Equal(expected.Location.Latitude, actual.Location.Latitude);
            Assert.Equal(expected.Location.Longitude, actual.Location.Longitude);
            Assert.Equal(expected.Location.Altitude, actual.Location.Altitude);
            Assert.Equal(expected.MeteringMode, actual.MeteringMode);
            Assert.Equal(expected.Orientation, actual.Orientation);
            Assert.Equal(expected.Saturation, actual.Saturation);
            Assert.Equal(expected.SceneCaptureType, actual.SceneCaptureType);
            Assert.Equal(expected.SensingMethod, actual.SensingMethod);
            Assert.Equal(expected.ShutterSpeedValue, actual.ShutterSpeedValue);
            Assert.Equal(expected.SubjectDistanceRange, actual.SubjectDistanceRange);
            Assert.Equal(expected.Size.Width, actual.Size.Width);
            Assert.Equal(expected.Size.Height, actual.Size.Height);
            Assert.Equal(expected.WhiteBalance, actual.WhiteBalance);
        }
    }
}
