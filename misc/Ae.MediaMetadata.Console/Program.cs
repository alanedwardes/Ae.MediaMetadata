using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;

namespace Ae.MediaMetadata.Console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            IExifReader reader = args[0].ToLowerInvariant() switch
            {
                "ffmpeg" => new FfmpegExifReader(),
                "imagesharp" => new ImageSharpExifReader(),
                _ => throw new System.Exception($"Invalid parser {args[0]}"),
            };

            var inputFile = new FileInfo(args[1]);

            if (reader is FfmpegExifReader)
            {
                var ffmpegPath = Path.Combine(Path.GetTempPath(), "ffmpeg");
                await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official, ffmpegPath);
                FFmpeg.SetExecutablesPath(ffmpegPath);
            }

            var result = await reader.ReadMediaInfo(inputFile, CancellationToken.None);

            System.Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions{WriteIndented = true}));
        }
    }
}
