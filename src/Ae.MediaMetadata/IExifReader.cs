using Ae.MediaMetadata.Entities;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Ae.MediaMetadata
{
    public interface IExifReader
    {
        Task<MediaInfo> ReadMediaInfo(FileInfo fileInfo, CancellationToken token);
    }
}
