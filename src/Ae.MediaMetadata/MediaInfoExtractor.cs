using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace Ae.MediaMetadata
{
    public sealed class MediaInfoExtractor : IMediaInfoExtractor
    {
        private readonly ILogger<MediaInfoExtractor> _logger;

        public MediaInfoExtractor(ILogger<MediaInfoExtractor> logger)
        {
            _logger = logger;
        }

        public MediaInfoExtractor() : this(NullLogger<MediaInfoExtractor>.Instance)
        {
        }

        public async Task ExtractSnapshot(FileInfo input, FileInfo output, CancellationToken token)
        {
            var conversion = await FFmpeg.Conversions.FromSnippet.Snapshot(input.FullName, output.FullName, TimeSpan.Zero);
            await conversion.Start(token);
        }

        public async Task<Entities.MediaInfo> ExtractInformation(FileInfo fileInfo, CancellationToken token)
        {
            IExifReader imageSharpReader = new ImageSharpExifReader(_logger);
            IExifReader ffmpegReader = new FfmpegExifReader(_logger);

            // Don't bother trying both when we know which reader should be used
            var extensionReaderMap = new Dictionary<string, IExifReader>
            {
                { ".mp4", ffmpegReader },
                { ".mov", ffmpegReader }
            };

            var readers = new[] { imageSharpReader, ffmpegReader };
            if (extensionReaderMap.TryGetValue(fileInfo.Extension.ToLowerInvariant(), out var knownReader))
            {
                readers = new[] { knownReader };
            }

            foreach (var exifReader in readers)
            {
                try
                {
                    return await exifReader.ReadMediaInfo(fileInfo, token);
                }
                catch (Exception)
                {
                    // Continue
                }
            }

            throw new NotImplementedException($"Information for {fileInfo} could not be generated");
        }

        public TPixel ExtractColor<TPixel>(Image<TPixel> image) where TPixel : unmanaged, IPixel<TPixel>
        {
            using var clone = image.Clone(x => x.Resize(1, 1));
            return clone[0, 0];
        }
    }
}
