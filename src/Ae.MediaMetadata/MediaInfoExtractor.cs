using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
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
            var exifReaders = new IExifReader[]
            {
                new ImageSharpExifReader(_logger),
                new FfmpegExifReader(_logger)
            };

            foreach (var exifReader in exifReaders)
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

            throw new NotImplementedException();
        }
    }
}
