using Ae.MediaMetadata.Entities;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ae.MediaMetadata.Tests
{
    public sealed class PhotoTests : IClassFixture<FfmpegFixture>
    {
        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto1(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_0293.jpg");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Equal(0.02d, mediaInfo.ExposureTime);
            Assert.Equal(4.2d, mediaInfo.FocalLength);
            Assert.Equal((ushort)26, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.6d, mediaInfo.FStop);
            Assert.Equal(125u, mediaInfo.IsoSpeed);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(5.6440726115735345d, mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.CompulsoryFlashMode | MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("11/11/2021 18:05:38 +00:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Equal(MediaSceneCaptureType.Standard, mediaInfo.SceneCaptureType);
            Assert.Equal(MediaSensingMethod.OneChipColorAreaSensor, mediaInfo.SensingMethod);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto2(Type exifReader)
        {
            var file = new FileInfo("Files/5528.JPEG");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

            Assert.Equal("samsung", mediaInfo.CameraMake);
            Assert.Equal("SM-G960F", mediaInfo.CameraModel);
            Assert.Equal("G960FXXU8DTC5", mediaInfo.CameraSoftware);
            Assert.Equal(4032, mediaInfo.Size.Width);
            Assert.Equal(3024, mediaInfo.Size.Height);
            Assert.Equal(1.16d, mediaInfo.ApertureValue);
            Assert.Equal(4.66d, mediaInfo.BrightnessValue);
            Assert.Equal(MediaContrast.Normal, mediaInfo.Contrast);
            Assert.Equal(double.NaN, mediaInfo.DigitalZoomRatio);
            Assert.Equal(0f, mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NormalProgram, mediaInfo.ExposureProgram);
            Assert.Equal(0.005917159763313609d, mediaInfo.ExposureTime);
            Assert.Equal(4.3d, mediaInfo.FocalLength);
            Assert.Equal((ushort)26, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.5d, mediaInfo.FStop);
            Assert.Equal(50u, mediaInfo.IsoSpeed);
            Assert.Equal(MediaMeteringMode.CenterWeightedAverage, mediaInfo.MeteringMode);
            Assert.Equal(MediaSaturation.Normal, mediaInfo.Saturation);
            Assert.Equal(7.4d, mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.RightTop, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.FlashFired, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("25/03/2021 17:38:08 +00:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Equal(MediaSceneCaptureType.Standard, mediaInfo.SceneCaptureType);
            Assert.Null(mediaInfo.SensingMethod);
            Assert.Equal("H12LLKF00SM", mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto3(Type exifReader)
        {
            var file = new FileInfo("Files/495.JPEG");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Equal(0.04d, mediaInfo.ExposureTime);
            Assert.Equal(4.2d, mediaInfo.FocalLength);
            Assert.Equal((ushort)26, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.7d, mediaInfo.FStop);
            Assert.Equal(250u, mediaInfo.IsoSpeed);
            Assert.Equal(MediaMeteringMode.CenterWeightedAverage, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(4.64d, mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.RightTop, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.FlashDidNotFire, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("17/03/2018 12:49:35 +00:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Equal(MediaSceneCaptureType.Standard, mediaInfo.SceneCaptureType);
            Assert.Null(mediaInfo.SensingMethod);
            Assert.Equal("C12LLJB18SM C12LLKH01GM", mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto4(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_3228.JPG");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Equal(0.03333333333333333d, mediaInfo.ExposureTime);
            Assert.Equal(3.99d, mediaInfo.FocalLength);
            Assert.Equal((ushort)28, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.8d, mediaInfo.FStop);
            Assert.Equal(20u, mediaInfo.IsoSpeed);
            Assert.Equal(12.02624703087886, mediaInfo.Location.Altitude);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(4.907684296659972d, mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Equal(53.37292777803209, mediaInfo.Location.Latitude);
            Assert.Equal(-0.9346972221798368, mediaInfo.Location.Longitude);
            Assert.Equal(DateTimeOffset.Parse("10/10/2021 11:26:45 +01:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Equal(MediaSceneCaptureType.Standard, mediaInfo.SceneCaptureType);
            Assert.Equal(MediaSensingMethod.OneChipColorAreaSensor, mediaInfo.SensingMethod);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto5(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_3285.JPG");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Equal(0.01d, mediaInfo.ExposureTime);
            Assert.Equal(6.6d, mediaInfo.FocalLength);
            Assert.Equal((ushort)57, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2.8d, mediaInfo.FStop);
            Assert.Equal(80u, mediaInfo.IsoSpeed);
            Assert.Equal(20.366126205083262d, mediaInfo.Location.Altitude);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(6.645877378435518d, mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.RightTop, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Equal(53.30085277775923, mediaInfo.Location.Latitude);
            Assert.Equal(0.1541388887829251, mediaInfo.Location.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2021-10-23T18:10:50.0000000+01:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Equal(MediaSceneCaptureType.Standard, mediaInfo.SceneCaptureType);
            Assert.Equal(MediaSensingMethod.OneChipColorAreaSensor, mediaInfo.SensingMethod);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto6(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_20211111_182849_Original.jpeg");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Equal((ushort)24, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.7d, mediaInfo.FStop);
            Assert.Equal(1000u, mediaInfo.IsoSpeed);
            Assert.Equal(MediaMeteringMode.Unknown, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(5.643d, mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Null(mediaInfo.Orientation);
            Assert.Equal(MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("11/11/2021 18:28:49 +00:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Null(mediaInfo.SceneCaptureType);
            Assert.Equal(MediaSensingMethod.OneChipColorAreaSensor, mediaInfo.SensingMethod);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto7(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_20170816_131016.jpg");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Equal(0.000678166, mediaInfo.ExposureTime);
            Assert.Equal(3.82d, mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2d, mediaInfo.FStop);
            Assert.Equal(40u, mediaInfo.IsoSpeed);
            Assert.Equal(1019, mediaInfo.Location.Altitude);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(-10.5200000, mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.FlashDidNotFire, mediaInfo.Flash);
            Assert.Equal(42.35649722205268, mediaInfo.Location.Latitude);
            Assert.Equal(23.38378333333466, mediaInfo.Location.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2017-08-16T10:10:13.0000000+01:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Equal(MediaSubjectDistanceRange.Distant, mediaInfo.SubjectDistanceRange);
            Assert.Null(mediaInfo.SceneCaptureType);
            Assert.Null(mediaInfo.SensingMethod);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto8(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_20160317_184950.jpg");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Equal(0.008315366, mediaInfo.ExposureTime);
            Assert.Equal(3.82d, mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2d, mediaInfo.FStop);
            Assert.Equal(72u, mediaInfo.IsoSpeed);
            Assert.Equal(17, mediaInfo.Location.Altitude);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(-6.91, mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Equal(42.36251944435968, mediaInfo.Location.Latitude);
            Assert.Equal(-71.0559500000212, mediaInfo.Location.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2016-03-17T22:49:10.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Null(mediaInfo.SceneCaptureType);
            Assert.Null(mediaInfo.SensingMethod);
            Assert.Equal("86831a0ac1a3f7a90000000000000000", mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto9(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_20160319_131826.jpg");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Equal(0.003203033, mediaInfo.ExposureTime);
            Assert.Equal(3.82d, mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2d, mediaInfo.FStop);
            Assert.Equal(40u, mediaInfo.IsoSpeed);
            Assert.Equal(32, mediaInfo.Location.Altitude);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(-8.28, mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Equal(42.61323611153497, mediaInfo.Location.Latitude);
            Assert.Equal(-70.63416388889154, mediaInfo.Location.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2016-03-19T17:18:23.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Null(mediaInfo.SceneCaptureType);
            Assert.Null(mediaInfo.SensingMethod);
            Assert.Equal("34050ed0f76953190000000000000000", mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto10(Type exifReader)
        {
            var file = new FileInfo("Files/100_0657.JPG");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Equal(0.006077, mediaInfo.ExposureTime);
            Assert.Equal(13.5d, mediaInfo.FocalLength);
            Assert.Equal((ushort)82, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(4d, mediaInfo.FStop);
            Assert.Equal(80u, mediaInfo.IsoSpeed);
            Assert.Equal(MediaMeteringMode.CenterWeightedAverage, mediaInfo.MeteringMode);
            Assert.Equal(MediaSaturation.Normal, mediaInfo.Saturation);
            Assert.Equal(7.36, mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.CompulsoryFlashMode | MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("2005-03-26T23:00:19.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Equal(MediaSubjectDistanceRange.Unknown, mediaInfo.SubjectDistanceRange);
            Assert.Equal(MediaSceneCaptureType.Standard, mediaInfo.SceneCaptureType);
            Assert.Equal(MediaSensingMethod.OneChipColorAreaSensor, mediaInfo.SensingMethod);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto11(Type exifReader)
        {
            var file = new FileInfo("Files/06-08-06_1242.jpg");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Null(mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Null(mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Null(mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Null(mediaInfo.SceneCaptureType);
            Assert.Null(mediaInfo.SensingMethod);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto12(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_5197.JPG");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Equal(0.0010683760683760685, mediaInfo.ExposureTime);
            Assert.Equal(4.25d, mediaInfo.FocalLength);
            Assert.Equal((ushort)26, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(1.8d, mediaInfo.FStop);
            Assert.Equal(25u, mediaInfo.IsoSpeed);
            Assert.Equal(20.345983787767132, mediaInfo.Location.Altitude);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(9.870872641509434, mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Equal(53.30091944442855, mediaInfo.Location.Latitude);
            Assert.Equal(0.15401944451861913, mediaInfo.Location.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2021-10-23T10:29:39.0000000+01:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Equal(MediaSceneCaptureType.Standard, mediaInfo.SceneCaptureType);
            Assert.Equal(MediaSensingMethod.OneChipColorAreaSensor, mediaInfo.SensingMethod);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto13(Type exifReader)
        {
            var file = new FileInfo("Files/DSC02135.JPG");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

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
            Assert.Equal(0.025, mediaInfo.ExposureTime);
            Assert.Equal(7.6d, mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2.8d, mediaInfo.FStop);
            Assert.Equal(200u, mediaInfo.IsoSpeed);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Equal(MediaSaturation.Normal, mediaInfo.Saturation);
            Assert.Null(mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.CompulsoryFlashMode | MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("2021-11-29T17:14:01.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Equal(MediaSceneCaptureType.Standard, mediaInfo.SceneCaptureType);
            Assert.Null(mediaInfo.SensingMethod);
            Assert.Null(mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto14(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_0455.JPG");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

            Assert.Equal("Apple", mediaInfo.CameraMake);
            Assert.Equal("iPhone 4S", mediaInfo.CameraModel);
            Assert.Equal("5.1.1", mediaInfo.CameraSoftware);
            Assert.Equal(3264, mediaInfo.Size.Width);
            Assert.Equal(2448, mediaInfo.Size.Height);
            Assert.Equal(2.5260688216892597, mediaInfo.ApertureValue);
            Assert.Equal(8.205275229357799, mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NormalProgram, mediaInfo.ExposureProgram);
            Assert.Equal(0.002320185614849188, mediaInfo.ExposureTime);
            Assert.Equal(4.28d, mediaInfo.FocalLength);
            Assert.Equal((ushort)35, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2.4d, mediaInfo.FStop);
            Assert.Equal(64u, mediaInfo.IsoSpeed);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(8.752281616688396, mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Equal(53.3789999961853, mediaInfo.Location.Latitude);
            Assert.Equal(-1.4796666781107584, mediaInfo.Location.Longitude);
            Assert.Equal(90, mediaInfo.Location.Altitude);
            Assert.Equal(DateTimeOffset.Parse("2012-06-28T14:19:02.0000000+01:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Equal(MediaSceneCaptureType.Standard, mediaInfo.SceneCaptureType);
            Assert.Equal(MediaSensingMethod.OneChipColorAreaSensor, mediaInfo.SensingMethod);
            Assert.Equal("be7cd6bb6f60371b0000000000000000", mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto15(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_0922.JPG");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

            Assert.Null(mediaInfo.CameraMake);
            Assert.Null(mediaInfo.CameraModel);
            Assert.Equal("Facebook for iPhone/iPad", mediaInfo.CameraSoftware);
            Assert.Equal(3264, mediaInfo.Size.Width);
            Assert.Equal(2448, mediaInfo.Size.Height);
            Assert.Equal(2.5260688216892597, mediaInfo.ApertureValue);
            Assert.Equal(0.4564386702330913, mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Equal(MediaExposureProgram.NormalProgram, mediaInfo.ExposureProgram);
            Assert.Equal(0.058823529411764705, mediaInfo.ExposureTime);
            Assert.Equal(4.28d, mediaInfo.FocalLength);
            Assert.Equal((ushort)35, mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2.4d, mediaInfo.FStop);
            Assert.Equal(400u, mediaInfo.IsoSpeed);
            Assert.Equal(MediaMeteringMode.Pattern, mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(4.099544072948328, mediaInfo.ShutterSpeedValue);
            Assert.Equal(MediaWhiteBalance.Auto, mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(MediaFlash.CompulsoryFlashMode | MediaFlash.AutoMode, mediaInfo.Flash);
            Assert.Equal(53, mediaInfo.Location.Latitude);
            Assert.Equal(-1.4692444443702697, mediaInfo.Location.Longitude);
            Assert.Equal(77, mediaInfo.Location.Altitude);
            Assert.Equal(DateTimeOffset.Parse("2013-10-09T23:54:04.0000000+01:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Equal(MediaSceneCaptureType.Standard, mediaInfo.SceneCaptureType);
            Assert.Equal(MediaSensingMethod.OneChipColorAreaSensor, mediaInfo.SensingMethod);
            Assert.Equal("542458f4ea0783650000000000000000", mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto16(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_20151231_123507.jpg");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

            Assert.Equal("motorola", mediaInfo.CameraMake);
            Assert.Equal("Nexus 6", mediaInfo.CameraModel);
            Assert.Equal("Picasa", mediaInfo.CameraSoftware);
            Assert.Equal(4160, mediaInfo.Size.Width);
            Assert.Equal(2340, mediaInfo.Size.Height);
            Assert.Equal(2, mediaInfo.ApertureValue);
            Assert.Null(mediaInfo.BrightnessValue);
            Assert.Null(mediaInfo.Contrast);
            Assert.Null(mediaInfo.DigitalZoomRatio);
            Assert.Null(mediaInfo.ExposureBias);
            Assert.Null(mediaInfo.ExposureIndex);
            Assert.Null(mediaInfo.ExposureProgram);
            Assert.Equal(0.001387633, mediaInfo.ExposureTime);
            Assert.Equal(3.82d, mediaInfo.FocalLength);
            Assert.Null(mediaInfo.FocalLengthIn35mmFilm);
            Assert.Equal(2, mediaInfo.FStop);
            Assert.Equal(40u, mediaInfo.IsoSpeed);
            Assert.Null(mediaInfo.MeteringMode);
            Assert.Null(mediaInfo.Saturation);
            Assert.Equal(-9.49, mediaInfo.ShutterSpeedValue);
            Assert.Null(mediaInfo.WhiteBalance);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Equal(53.381044443978205, mediaInfo.Location.Latitude);
            Assert.Equal(-1.4699027776718139, mediaInfo.Location.Longitude);
            Assert.Null(mediaInfo.Location.Altitude);
            Assert.Equal(DateTimeOffset.Parse("2015-12-31T12:35:00.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Null(mediaInfo.SceneCaptureType);
            Assert.Null(mediaInfo.SensingMethod);
            Assert.Equal("e2d8f42014aa6d450000000000000000", mediaInfo.ImageUniqueId);
        }

        [Theory]
        [InlineData(typeof(ImageSharpExifReader))]
        [InlineData(typeof(FfmpegExifReader))]
        public async Task TestPhoto17(Type exifReader)
        {
            var file = new FileInfo("Files/IMG_3263.JPG");

            var mediaInfo = await ((IExifReader)Activator.CreateInstance(exifReader)).ReadMediaInfo(file, CancellationToken.None);

            Assert.Null(mediaInfo.CameraMake);
            Assert.Null(mediaInfo.CameraModel);
            Assert.Null(mediaInfo.CameraSoftware);
            Assert.Equal(4000, mediaInfo.Size.Width);
            Assert.Equal(3000, mediaInfo.Size.Height);
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
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Flash);
            Assert.Null(mediaInfo.Location);
            Assert.Null(mediaInfo.CreationTime);
            Assert.Null(mediaInfo.Duration);
            Assert.Null(mediaInfo.SubjectDistanceRange);
            Assert.Equal(MediaSceneCaptureType.Standard, mediaInfo.SceneCaptureType);
            Assert.Null(mediaInfo.SensingMethod);
            Assert.Null(mediaInfo.ImageUniqueId);
        }
    }
}
