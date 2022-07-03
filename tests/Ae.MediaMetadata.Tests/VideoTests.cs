using Ae.MediaMetadata.Entities;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ae.MediaMetadata.Tests
{
    public sealed class VideoTests : IClassFixture<FfmpegFixture>
    {
        private readonly IMediaInfoExtractor _mediaInfoExtractor = new MediaInfoExtractor();

        [Fact]
        public async Task Video1()
        {
            var file = new FileInfo("Files/VID_20161119_205638.mp4");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal(1920, mediaInfo.Size.Width);
            Assert.Equal(1080, mediaInfo.Size.Height);
            Assert.Null(mediaInfo.Orientation);
            Assert.Null(mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Null(mediaInfo.ExposureProgram);
            Assert.Null(mediaInfo.ExposureTime);
            Assert.Null(mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Null(mediaInfo.FStop);
            Assert.Null(mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.Location.Altitude);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Null(mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Null(mediaInfo.Flash);
            Assert.Null(mediaInfo.CameraMake);
            Assert.Null(mediaInfo.CameraModel);
            Assert.Null(mediaInfo.CameraSoftware);
            Assert.Equal(42.36099999999999, mediaInfo.Location.Latitude);
            Assert.Equal(-71.0543, mediaInfo.Location.Longitude);
            Assert.Equal(DateTimeOffset.Parse("05/10/2018 22:17:25 +00:00"), mediaInfo.CreationTime);
            Assert.Equal(15.3456106f, mediaInfo.Duration);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Fact]
        public async Task Video2()
        {
            var file = new FileInfo("Files/IMG_3285.MOV");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal(1440, mediaInfo.Size.Width);
            Assert.Equal(1080, mediaInfo.Size.Height);
            Assert.Null(mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Null(mediaInfo.ExposureProgram);
            Assert.Null(mediaInfo.ExposureTime);
            Assert.Null(mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Null(mediaInfo.FStop);
            Assert.Null(mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.Location.Altitude);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Null(mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.LeftBottom, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Equal("Apple", mediaInfo.CameraMake);
            Assert.Equal("iPhone 8 Plus", mediaInfo.CameraModel);
            Assert.Equal("15.0.2", mediaInfo.CameraSoftware);
            Assert.Equal(53.3009, mediaInfo.Location.Latitude);
            Assert.Equal(0.1541, mediaInfo.Location.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2021-10-23T17:10:50.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Equal(2.798333f, mediaInfo.Duration);
            Assert.Equal("7158DA5B-EBDA-4DD4-86F1-105920D4C8D0", mediaInfo.ImageUniqueId);
        }

        [Fact]
        public async Task Video3()
        {
            var file = new FileInfo("Files/100_2501.MOV");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal(320, mediaInfo.Size.Width);
            Assert.Equal(240, mediaInfo.Size.Height);
            Assert.Null(mediaInfo.Orientation);
            Assert.Null(mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Null(mediaInfo.ExposureProgram);
            Assert.Null(mediaInfo.ExposureTime);
            Assert.Null(mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Null(mediaInfo.FStop);
            Assert.Null(mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Null(mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Null(mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Null(mediaInfo.CameraMake);
            Assert.Null(mediaInfo.CameraModel);
            Assert.Null(mediaInfo.CameraSoftware);
            Assert.Equal(DateTimeOffset.Parse("2005-04-22T08:12:17.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Equal(19.25f, mediaInfo.Duration);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Fact]
        public async Task Video4()
        {
            var file = new FileInfo("Files/SAM_0146.MP4");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal(1280, mediaInfo.Size.Width);
            Assert.Equal(720, mediaInfo.Size.Height);
            Assert.Null(mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Null(mediaInfo.ExposureProgram);
            Assert.Null(mediaInfo.ExposureTime);
            Assert.Null(mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Null(mediaInfo.FStop);
            Assert.Null(mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Null(mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Null(mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Null(mediaInfo.CameraMake);
            Assert.Null(mediaInfo.CameraModel);
            Assert.Null(mediaInfo.CameraSoftware);
            Assert.Equal(DateTimeOffset.Parse("2010-04-09T16:56:52.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Equal(18.733334f, mediaInfo.Duration);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Fact]
        public async Task Video5()
        {
            var file = new FileInfo("Files/IMG_3376.MOV");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal(1440, mediaInfo.Size.Width);
            Assert.Equal(1080, mediaInfo.Size.Height);
            Assert.Null(mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Null(mediaInfo.ExposureProgram);
            Assert.Null(mediaInfo.ExposureTime);
            Assert.Null(mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Null(mediaInfo.FStop);
            Assert.Null(mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Null(mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.LeftBottom, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Equal("Apple", mediaInfo.CameraMake);
            Assert.Equal("iPhone 8 Plus", mediaInfo.CameraModel);
            Assert.Equal("11.4", mediaInfo.CameraSoftware);
            Assert.Equal(43.6217, mediaInfo.Location.Latitude);
            Assert.Equal(22.681, mediaInfo.Location.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2018-07-13T09:20:59.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Equal(2.28f, mediaInfo.Duration);
            Assert.Equal("75DF1AF7-ED43-47A1-84FA-15F707D7D320", mediaInfo.ImageUniqueId);
        }

        [Fact]
        public async Task Video6()
        {
            var file = new FileInfo("Files/VID_20160405_113259.mp4");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal(1920, mediaInfo.Size.Width);
            Assert.Equal(1080, mediaInfo.Size.Height);
            Assert.Null(mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Null(mediaInfo.ExposureProgram);
            Assert.Null(mediaInfo.ExposureTime);
            Assert.Null(mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Null(mediaInfo.FStop);
            Assert.Null(mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Null(mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Null(mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Null(mediaInfo.CameraMake);
            Assert.Null(mediaInfo.CameraModel);
            Assert.Null(mediaInfo.CameraSoftware);
            Assert.Equal(43.0858, mediaInfo.Location.Latitude);
            Assert.Equal(-79.0685, mediaInfo.Location.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2016-04-05T15:33:06.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Equal(6.323633f, mediaInfo.Duration);
            Assert.Null(mediaInfo.ImageUniqueId);
        }
    }
}
