using Ae.MediaMetadata.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            IExifReader[] exifReaders = new[] { imageSharpReader, ffmpegReader };

            // If it looks like a video, use ffmpeg
            if (FileExtensions.Videos.Contains(fileInfo.Extension, StringComparer.InvariantCultureIgnoreCase))
            {
                exifReaders = new[] { ffmpegReader };
            }

            var exceptions = new List<Exception>();

            foreach (var exifReader in exifReaders)
            {
                try
                {
                    return await exifReader.ReadMediaInfo(fileInfo, token);
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            throw new AggregateException($"Information for {fileInfo} could not be generated", exceptions);
        }

        public TPixel ExtractColor<TPixel>(Image<TPixel> image) where TPixel : unmanaged, IPixel<TPixel>
        {
            using var clone = image.Clone(x => x.Resize(1, 1));
            return clone[0, 0];
        }
    }
}
