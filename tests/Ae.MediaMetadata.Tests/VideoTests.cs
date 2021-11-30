﻿using Ae.Galeriya.Core;
using Ae.Galeriya.Core.Entities;
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
            Assert.Equal(MediaOrientation.Unknown, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Camera.Make);
            Assert.Null(mediaInfo.Camera.Model);
            Assert.Null(mediaInfo.Camera.Software);
            Assert.Equal(42.361000061035156, mediaInfo.Location.Value.Latitude);
            Assert.Equal(-71.0542984008789, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("05/10/2018 22:17:25 +00:00"), mediaInfo.CreationTime);
            Assert.Equal(15.3456106f, mediaInfo.Duration);
        }

        [Fact]
        public async Task Video2()
        {
            var file = new FileInfo("Files/IMG_3285.MOV");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal(1440, mediaInfo.Size.Width);
            Assert.Equal(1080, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.Unknown, mediaInfo.Orientation);
            Assert.Equal("Apple", mediaInfo.Camera.Make);
            Assert.Equal("iPhone 8 Plus", mediaInfo.Camera.Model);
            Assert.Equal("15.0.2", mediaInfo.Camera.Software);
            Assert.Equal(53.300899505615234, mediaInfo.Location.Value.Latitude);
            Assert.Equal(0.15410000085830688, mediaInfo.Location.Value.Longitude);
            Assert.Equal(DateTimeOffset.Parse("2021-10-23T17:10:50.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Equal(2.798333f, mediaInfo.Duration);
        }

        [Fact]
        public async Task Video3()
        {
            var file = new FileInfo("Files/100_2501.MOV");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal(320, mediaInfo.Size.Width);
            Assert.Equal(240, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.Unknown, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Location);
            Assert.Null(mediaInfo.Camera.Make);
            Assert.Null(mediaInfo.Camera.Model);
            Assert.Null(mediaInfo.Camera.Software);
            Assert.Equal(DateTimeOffset.Parse("2005-04-22T08:12:17.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Equal(19.25f, mediaInfo.Duration);
        }

        [Fact]
        public async Task Video4()
        {
            var file = new FileInfo("Files/SAM_0146.MP4");

            var mediaInfo = await _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None);

            Assert.Equal(1280, mediaInfo.Size.Width);
            Assert.Equal(720, mediaInfo.Size.Height);
            Assert.Equal(MediaOrientation.Unknown, mediaInfo.Orientation);
            Assert.Null(mediaInfo.Location);
            Assert.Null(mediaInfo.Camera.Make);
            Assert.Null(mediaInfo.Camera.Model);
            Assert.Null(mediaInfo.Camera.Software);
            Assert.Equal(DateTimeOffset.Parse("2010-04-09T16:56:52.0000000+00:00"), mediaInfo.CreationTime);
            Assert.Equal(18.733334f, mediaInfo.Duration);
        }
    }
}
