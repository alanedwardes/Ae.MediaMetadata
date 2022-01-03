using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace Ae.MediaMetadata.Tests
{
    public sealed class ColourTests
    {
        [Fact]
        public void Image1()
        {
            var image = Image.Load<Rgba32>(@"Files/IMG_20160317_184950.jpg");

            var color = new MediaInfoExtractor().ExtractColor(image);

            Assert.Equal(new Rgba32(94, 72, 58, 255), color);
        }
    }
}
