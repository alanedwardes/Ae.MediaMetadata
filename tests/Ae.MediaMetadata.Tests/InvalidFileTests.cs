using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Ae.MediaMetadata.Tests
{
    public sealed class InvalidFileTests : IClassFixture<FfmpegFixture>
    {
        private readonly IMediaInfoExtractor _mediaInfoExtractor = new MediaInfoExtractor();

        [Fact]
        public async Task TestInvalid1()
        {
            var file = new FileInfo("Files/IMG_1812.HEIC");

            await Assert.ThrowsAsync<AggregateException>(() => _mediaInfoExtractor.ExtractInformation(file, CancellationToken.None));
        }
    }
}
