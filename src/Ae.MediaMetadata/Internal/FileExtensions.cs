namespace Ae.MediaMetadata.Internal
{
    internal static class FileExtensions
    {
        public static string[] Videos { get; } = new[] { ".mp4", ".mov", ".avi", ".wmv", ".avchd", ".flv", ".mkv" };
        public static string[] Images { get; } = new[] { ".jpg", ".jpeg", ".png", ".gif", ".tiff", ".tga", ".webp", ".bmp", ".heic", ".heif", ".jp2", ".j2k", ".jpf", ".jpx", ".jpm", ".mj2" };
    }
}
