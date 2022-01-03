using Ae.MediaMetadata.Entities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ae.MediaMetadata
{
    public interface IMediaInfoExtractor
    {
        TPixel ExtractColor<TPixel>(Image<TPixel> image) where TPixel : unmanaged, IPixel<TPixel>;
        Task<MediaInfo> ExtractInformation(FileInfo fileInfo, CancellationToken token);
        Task ExtractSnapshot(FileInfo fileInfo, FileInfo output, CancellationToken token);
    }
}