using Xabe.FFmpeg.Downloader;

namespace Ae.MediaMetadata.Tests
{
    public sealed class FfmpegFixture
    {
        public FfmpegFixture()
        {
            FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official).GetAwaiter().GetResult();
        }
    }
}
