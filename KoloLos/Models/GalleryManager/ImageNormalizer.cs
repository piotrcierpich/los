using System.Drawing;

namespace KoloLos.Models.GalleryManager
{
    public class ImageNormalizer
    {
        public const int NormalizedImageHeight = 768;
        public const int NormalizedImageWidth = 1024;

        private readonly string pathOfImage;

        public ImageNormalizer(string pathOfImage)
        {
            this.pathOfImage = pathOfImage;
        }

        public void NormalizeImage()
        {
            ImageResizer imageResizer = new ImageResizer(pathOfImage);
            imageResizer.ResizeAndSave(new Size(NormalizedImageWidth, NormalizedImageHeight));
        }
    }
}