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

            Assert.Equal("Apple", mediaInfo.Camera.Make);
            Assert.Equal("iPhone 12", mediaInfo.Camera.Model);
            Assert.Equal("14.8", mediaInfo.Camera.Software);
            Assert.Equal(4030, mediaInfo.Size.Width);
            Assert.Equal(2356, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("11/11/2021 18:05:38 +00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto2()
        {
            var file = new FileInfo("Files/5528.JPEG");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("samsung", mediaInfo.Camera.Make);
            Assert.Equal("SM-G960F", mediaInfo.Camera.Model);
            Assert.Equal("G960FXXU8DTC5", mediaInfo.Camera.Software);
            Assert.Equal(4032, mediaInfo.Size.Width);
            Assert.Equal(3024, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.RightTop, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("25/03/2021 17:38:08 +00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto3()
        {
            var file = new FileInfo("Files/495.JPEG");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("samsung", mediaInfo.Camera.Make);
            Assert.Equal("SM-G930F", mediaInfo.Camera.Model);
            Assert.Equal("G930FXXU1DQJ3", mediaInfo.Camera.Software);
            Assert.Equal(4032, mediaInfo.Size.Width);
            Assert.Equal(3024, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.RightTop, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("17/03/2018 12:49:35 +00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto4()
        {
            var file = new FileInfo("Files/IMG_3228.JPG");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("Apple", mediaInfo.Camera.Make);
            Assert.Equal("iPhone 8 Plus", mediaInfo.Camera.Model);
            Assert.Equal("15.0.1", mediaInfo.Camera.Software);
            Assert.Equal(4032, mediaInfo.Size.Width);
            Assert.Equal(3024, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(53.372928619384766, mediaInfo.Location.Value.Latitude);
            Assert.Equal(-0.9346972107887268, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("10/10/2021 11:26:45 +01:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto5()
        {
            var file = new FileInfo("Files/IMG_3285.JPG");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("Apple", mediaInfo.Camera.Make);
            Assert.Equal("iPhone 8 Plus", mediaInfo.Camera.Model);
            Assert.Equal("15.0.2", mediaInfo.Camera.Software);
            Assert.Equal(4032, mediaInfo.Size.Width);
            Assert.Equal(3024, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.RightTop, mediaInfo.Orientation);
            Assert.Equal(53.30084991455078, mediaInfo.Location.Value.Latitude);
            Assert.Equal(0.15413889288902283, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2021-10-23T18:10:50.0000000+01:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto6()
        {
            var file = new FileInfo("Files/IMG_20211111_182849_Original.jpeg");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("OnePlus", mediaInfo.Camera.Make);
            Assert.Equal("ONEPLUS A5000", mediaInfo.Camera.Model);
            Assert.Equal("OnePlus5-user 10 QKQ1.191014.012 2010292059 release-keys", mediaInfo.Camera.Software);
            Assert.Equal(3456, mediaInfo.Size.Width);
            Assert.Equal(4608, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.Unknown, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Location);
            Assert.Equal(DateTimeOffset.Parse("11/11/2021 18:28:49 +00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto7()
        {
            var file = new FileInfo("Files/IMG_20170816_131016.jpg");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("motorola", mediaInfo.Camera.Make);
            Assert.Equal("Nexus 6", mediaInfo.Camera.Model);
            Assert.Null(mediaInfo.Camera.Software);
            Assert.Equal(4160, mediaInfo.Size.Width);
            Assert.Equal(3120, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(42.35649871826172, mediaInfo.Location.Value.Latitude);
            Assert.Equal(23.3837833404541, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2017-08-16T10:10:13.0000000+01:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto8()
        {
            var file = new FileInfo("Files/IMG_20160317_184950.jpg");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("motorola", mediaInfo.Camera.Make);
            Assert.Equal("Nexus 6", mediaInfo.Camera.Model);
            Assert.Equal("Picasa", mediaInfo.Camera.Software);
            Assert.Equal(4160, mediaInfo.Size.Width);
            Assert.Equal(2340, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(42.362518310546875, mediaInfo.Location.Value.Latitude);
            Assert.Equal(-71.05595397949219, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2016-03-17T22:49:10.0000000+00:00"), mediaInfo.CreationTime);
        }

        [Fact]
        public async Task TestPhoto9()
        {
            var file = new FileInfo("Files/IMG_20160319_131826.jpg");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal("motorola", mediaInfo.Camera.Make);
            Assert.Equal("Nexus 6", mediaInfo.Camera.Model);
            Assert.Equal("Picasa", mediaInfo.Camera.Software);
            Assert.Equal(4160, mediaInfo.Size.Width);
            Assert.Equal(2340, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.TopLeft, mediaInfo.Orientation);
            Assert.Equal(42.61323928833008, mediaInfo.Location.Value.Latitude);
            Assert.Equal(-70.63416290283203, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2016-03-19T17:18:23.0000000+00:00"), mediaInfo.CreationTime);
        }
    }
}
