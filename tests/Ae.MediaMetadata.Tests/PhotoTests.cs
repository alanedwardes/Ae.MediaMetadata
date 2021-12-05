using Ae.Galeriya.Core;
using Ae.Galeriya.Core.Entities;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ae.MediaMetadata.Tests
{
    public sealed class PhotoTests : IClassFixture<FfmpegFixture>
    {
        private readonly IMediaInfoExtractor _mediaInfoExtractor = new MediaInfoExtractor();

        [Fact]
        public async Task TestPhoto1()
        {
            var file = new FileInfo("Files/IMG_0293.jpg");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("Apple", mediaInfo.CameraMake);
            Assert.Equal("iPhone 12", mediaInfo.CameraModel);
            Assert.Equal("14.8", mediaInfo.CameraSoftware);
            Assert.Equal(4030, mediaInfo.Size.Width);
            Assert.Equal(2356, mediaInfo.Size.Height);
            Assert.Equal(1.3561438092556088d, mediaInfo.ApertureValue);
            Assert.Equal(2.5501566256283237d, mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Equal(0f, mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NormalProgram, mediaInfo.ExposureProgram);
            Assert.Equal(TimeSpan.FromMilliseconds(20f), mediaInfo.ExposureTime);
            Assert.Equal(4.2d, mediaInfo.FocalLength);
            Assert.Equal(26, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.6d, mediaInfo.FStop);
            Assert.Equal(125, mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.LocationAltitude);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(TimeSpan.FromSeconds(5.6440726d), mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.CompulsoryFlashMode | MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("11/11/2021 18:05:38 +00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto2()
        {
            var file = new FileInfo("Files/5528.JPEG");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("samsung", mediaInfo.CameraMake);
            Assert.Equal("SM-G960F", mediaInfo.CameraModel);
            Assert.Equal("G960FXXU8DTC5", mediaInfo.CameraSoftware);
            Assert.Equal(4032, mediaInfo.Size.Width);
            Assert.Equal(3024, mediaInfo.Size.Height);
            Assert.Equal(1.16d, mediaInfo.ApertureValue);
            Assert.Equal(4.66d, mediaInfo.BrightnessValue);
            Assert.Equal(MediaContrast.Normal, mediaInfo.Contrast);
            Assert.Equal(0f, mediaInfo.DigitalZoomRatio);
            Assert.Equal(0f, mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NormalProgram, mediaInfo.ExposureProgram);
            Assert.Equal(TimeSpan.FromMilliseconds(5.91711d), mediaInfo.ExposureTime);
            Assert.Equal(4.3d, mediaInfo.FocalLength);
            Assert.Equal(26, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.5d, mediaInfo.FStop);
            Assert.Equal(50, mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.LocationAltitude);
            Assert.Equal(MediaMeteringMode.CenterWeightedAverage, mediaInfo.MeteringMode);
            Assert.Equal(MediaSaturation.Normal, mediaInfo.Saturation);
            Assert.Equal(TimeSpan.FromSeconds(7.4d), mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.RightTop, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.FlashFired, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("25/03/2021 17:38:08 +00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto3()
        {
            var file = new FileInfo("Files/495.JPEG");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("samsung", mediaInfo.CameraMake);
            Assert.Equal("SM-G930F", mediaInfo.CameraModel);
            Assert.Equal("G930FXXU1DQJ3", mediaInfo.CameraSoftware);
            Assert.Equal(4032, mediaInfo.Size.Width);
            Assert.Equal(3024, mediaInfo.Size.Height);
            Assert.Equal(1.53d, mediaInfo.ApertureValue);
            Assert.Equal(-0.12d, mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Equal(0f, mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NormalProgram, mediaInfo.ExposureProgram);
            Assert.Equal(TimeSpan.FromMilliseconds(40d), mediaInfo.ExposureTime);
            Assert.Equal(4.2d, mediaInfo.FocalLength);
            Assert.Equal(26, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.7d, mediaInfo.FStop);
            Assert.Equal(250, mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.LocationAltitude);
            Assert.Equal(MediaMeteringMode.CenterWeightedAverage, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(TimeSpan.FromSeconds(4.64d), mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.RightTop, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.FlashDidNotFire, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("17/03/2018 12:49:35 +00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto4()
        {
            var file = new FileInfo("Files/IMG_3228.JPG");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("Apple", mediaInfo.CameraMake);
            Assert.Equal("iPhone 8 Plus", mediaInfo.CameraModel);
            Assert.Equal("15.0.1", mediaInfo.CameraSoftware);
            Assert.Equal(4032, mediaInfo.Size.Width);
            Assert.Equal(3024, mediaInfo.Size.Height);
            Assert.Equal(1.6959938128383605d, mediaInfo.ApertureValue);
            Assert.Equal(4.1101674783228415d, mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Equal(0f, mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NormalProgram, mediaInfo.ExposureProgram);
            Assert.Equal(TimeSpan.FromMilliseconds(33.333333d), mediaInfo.ExposureTime);
            Assert.Equal(3.99d, mediaInfo.FocalLength);
            Assert.Equal(28, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.8d, mediaInfo.FStop);
            Assert.Equal(20, mediaInfo.IsoSpeed);
            Assert.Equal(12.02624703087886, mediaInfo.LocationAltitude);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(TimeSpan.FromSeconds(4.9076842d), mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Equal(53.372928619384766, mediaInfo.Location.Value.Latitude);
            Assert.Equal(-0.9346972107887268, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("10/10/2021 11:26:45 +01:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto5()
        {
            var file = new FileInfo("Files/IMG_3285.JPG");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("Apple", mediaInfo.CameraMake);
            Assert.Equal("iPhone 8 Plus", mediaInfo.CameraModel);
            Assert.Equal("15.0.2", mediaInfo.CameraSoftware);
            Assert.Equal(4032, mediaInfo.Size.Width);
            Assert.Equal(3024, mediaInfo.Size.Height);
            Assert.Equal(2.970853653853539d, mediaInfo.ApertureValue);
            Assert.Equal(5.51714668880516d, mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Equal(0f, mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NormalProgram, mediaInfo.ExposureProgram);
            Assert.Equal(TimeSpan.FromMilliseconds(10d), mediaInfo.ExposureTime);
            Assert.Equal(6.6d, mediaInfo.FocalLength);
            Assert.Equal(57, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2.8d, mediaInfo.FStop);
            Assert.Equal(80, mediaInfo.IsoSpeed);
            Assert.Equal(20.366126205083262d, mediaInfo.LocationAltitude);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(TimeSpan.FromSeconds(6.6458773d), mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.RightTop, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Equal(53.30084991455078, mediaInfo.Location.Value.Latitude);
            Assert.Equal(0.15413889288902283, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2021-10-23T18:10:50.0000000+01:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto6()
        {
            var file = new FileInfo("Files/IMG_20211111_182849_Original.jpeg");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("OnePlus", mediaInfo.CameraMake);
            Assert.Equal("ONEPLUS A5000", mediaInfo.CameraModel);
            Assert.Equal("OnePlus5-user 10 QKQ1.191014.012 2010292059 release-keys", mediaInfo.CameraSoftware);
            Assert.Equal(3456, mediaInfo.Size.Width);
            Assert.Equal(4608, mediaInfo.Size.Height);
            Assert.Equal(1.53d, mediaInfo.ApertureValue);
            Assert.Equal(3d, mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NotDefined, mediaInfo.ExposureProgram);
            Assert.Null(mediaInfo.ExposureTime);
            Assert.Equal(4.103d, mediaInfo.FocalLength);
            Assert.Equal(24, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.7d, mediaInfo.FStop);
            Assert.Equal(1000, mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.LocationAltitude);
            Assert.Equal(MediaMeteringMode.Unknown, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(TimeSpan.FromSeconds(5.643d), mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Null(mediaInfo.Orientation);
            Assert.Equal(MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("11/11/2021 18:28:49 +00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto7()
        {
            var file = new FileInfo("Files/IMG_20170816_131016.jpg");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("motorola", mediaInfo.CameraMake);
            Assert.Equal("Nexus 6", mediaInfo.CameraModel);
            Assert.Null(mediaInfo.CameraSoftware);
            Assert.Equal(4160, mediaInfo.Size.Width);
            Assert.Equal(3120, mediaInfo.Size.Height);
            Assert.Equal(2d, mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Null(mediaInfo.ExposureProgram);
            Assert.Equal(TimeSpan.FromMilliseconds(0.6781), mediaInfo.ExposureTime);
            Assert.Equal(3.82d, mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2d, mediaInfo.FStop);
            Assert.Equal(40, mediaInfo.IsoSpeed);
            Assert.Equal(1019, mediaInfo.LocationAltitude);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(TimeSpan.FromSeconds(-10.5200000), mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.FlashDidNotFire, mediaInfo.Flash);
            Assert.Equal(42.35649871826172, mediaInfo.Location.Value.Latitude);
            Assert.Equal(23.3837833404541, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2017-08-16T10:10:13.0000000+01:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto8()
        {
            var file = new FileInfo("Files/IMG_20160317_184950.jpg");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("motorola", mediaInfo.CameraMake);
            Assert.Equal("Nexus 6", mediaInfo.CameraModel);
            Assert.Equal("Picasa", mediaInfo.CameraSoftware);
            Assert.Equal(4160, mediaInfo.Size.Width);
            Assert.Equal(2340, mediaInfo.Size.Height);
            Assert.Equal(2d, mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Null(mediaInfo.ExposureProgram);
            Assert.Equal(TimeSpan.FromMilliseconds(8.3153), mediaInfo.ExposureTime);
            Assert.Equal(3.82d, mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2d, mediaInfo.FStop);
            Assert.Equal(72, mediaInfo.IsoSpeed);
            Assert.Equal(17, mediaInfo.LocationAltitude);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(TimeSpan.FromSeconds(-6.91), mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Equal(42.362518310546875, mediaInfo.Location.Value.Latitude);
            Assert.Equal(-71.05595397949219, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2016-03-17T22:49:10.0000000+00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto9()
        {
            var file = new FileInfo("Files/IMG_20160319_131826.jpg");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("motorola", mediaInfo.CameraMake);
            Assert.Equal("Nexus 6", mediaInfo.CameraModel);
            Assert.Equal("Picasa", mediaInfo.CameraSoftware);
            Assert.Equal(4160, mediaInfo.Size.Width);
            Assert.Equal(2340, mediaInfo.Size.Height);
            Assert.Equal(2d, mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Null(mediaInfo.ExposureProgram);
            Assert.Equal(TimeSpan.FromMilliseconds(3.203), mediaInfo.ExposureTime);
            Assert.Equal(3.82d, mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2d, mediaInfo.FStop);
            Assert.Equal(40, mediaInfo.IsoSpeed);
            Assert.Equal(32, mediaInfo.LocationAltitude);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(TimeSpan.FromSeconds(-8.28), mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Equal(42.61323928833008, mediaInfo.Location.Value.Latitude);
            Assert.Equal(-70.63416290283203, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2016-03-19T17:18:23.0000000+00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto10()
        {
            var file = new FileInfo("Files/100_0657.JPG");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("EASTMAN KODAK COMPANY", mediaInfo.CameraMake);
            Assert.Equal("KODAK C330 ZOOM DIGITAL CAMERA", mediaInfo.CameraModel);
            Assert.Equal("Version 1.0700", mediaInfo.CameraSoftware);
            Assert.Equal(2304, mediaInfo.Size.Width);
            Assert.Equal(1728, mediaInfo.Size.Height);
            Assert.Equal(4d, mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Equal(MediaContrast.Normal, mediaInfo.Contrast);
            Assert.Equal(0, mediaInfo.DigitalZoomRatio);
            Assert.Equal(0, mediaInfo.ExposureBias);
            Assert.Equal(80, mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NormalProgram, mediaInfo.ExposureProgram);
            Assert.Equal(TimeSpan.FromMilliseconds(6.077), mediaInfo.ExposureTime);
            Assert.Equal(13.5d, mediaInfo.FocalLength);
            Assert.Equal(82, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(4d, mediaInfo.FStop);
            Assert.Equal(80, mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.LocationAltitude);
            Assert.Equal(MediaMeteringMode.CenterWeightedAverage, mediaInfo.MeteringMode);
            Assert.Equal(MediaSaturation.Normal, mediaInfo.Saturation);
            Assert.Equal(TimeSpan.FromSeconds(7.36), mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.CompulsoryFlashMode | MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("2005-03-26T23:00:19.0000000+00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto11()
        {
            var file = new FileInfo("Files/06-08-06_1242.jpg");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Null(mediaInfo.CameraMake);
            Assert.Null(mediaInfo.CameraModel);
            Assert.Null(mediaInfo.CameraSoftware);
            Assert.Equal(640, mediaInfo.Size.Width);
            Assert.Equal(480, mediaInfo.Size.Height);
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
            Assert.Null(mediaInfo.LocationAltitude);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Null(mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Null(mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Null(mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto12()
        {
            var file = new FileInfo("Files/IMG_5197.JPG");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("Apple", mediaInfo.CameraMake);
            Assert.Equal("iPhone XR", mediaInfo.CameraModel);
            Assert.Equal("15.0.2", mediaInfo.CameraSoftware);
            Assert.Equal(4032, mediaInfo.Size.Width);
            Assert.Equal(3024, mediaInfo.Size.Height);
            Assert.Equal(1.6959938128383605d, mediaInfo.ApertureValue);
            Assert.Equal(8.750345508390918, mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Equal(0, mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NormalProgram, mediaInfo.ExposureProgram);
            Assert.Equal(TimeSpan.FromMilliseconds(1.0683), mediaInfo.ExposureTime);
            Assert.Equal(4.25d, mediaInfo.FocalLength);
            Assert.Equal(26, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.8d, mediaInfo.FStop);
            Assert.Equal(25, mediaInfo.IsoSpeed);
            Assert.Equal(20.345983787767132, mediaInfo.LocationAltitude);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(TimeSpan.FromSeconds(9.8708726), mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Equal(53.30091857910156, mediaInfo.Location.Value.Latitude);
            Assert.Equal(0.15401943027973175, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2021-10-23T10:29:39.0000000+01:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto13()
        {
            var file = new FileInfo("Files/DSC02135.JPG");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("SONY", mediaInfo.CameraMake);
            Assert.Equal("DSC-W300", mediaInfo.CameraModel);
            Assert.Null(mediaInfo.CameraSoftware);
            Assert.Equal(2048, mediaInfo.Size.Width);
            Assert.Equal(1536, mediaInfo.Size.Height);
            Assert.Null(mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Equal(MediaContrast.Normal, mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Equal(0, mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NormalProgram, mediaInfo.ExposureProgram);
            Assert.Equal(TimeSpan.FromMilliseconds(25), mediaInfo.ExposureTime);
            Assert.Equal(7.6d, mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2.8d, mediaInfo.FStop);
            Assert.Equal(200, mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.LocationAltitude);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Equal(MediaSaturation.Normal, mediaInfo.Saturation);
            Assert.Null(mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.CompulsoryFlashMode | MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("2021-11-29T17:14:01.0000000+00:00"), mediaInfo.CreationTime);
        }
    }
}
