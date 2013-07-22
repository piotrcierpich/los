using System.Drawing;
using System.IO;
using System.Web;

namespace KoloLos.Models.GalleryManager
{
    public class ImageThumbnail
    {
        private readonly string thumbnailDirectory;

        public const int ThumbnailHeight = 200;
        public const int ThumbnailWidth = 200;

        public ImageThumbnail(string thumbnailDirectory)
        {
            this.thumbnailDirectory = thumbnailDirectory;
        }

        public void CreateThumbnail(string path)
        {
            ImageResizer imageResizer = new ImageResizer(path);
            var thumbnailPath = GetThumbnailPath(path);
            imageResizer.ResizeAndSave(new Size(ThumbnailWidth, ThumbnailHeight), thumbnailPath);
        }

        private string GetThumbnailPath(string path)
        {
            string imageFileName = "thumb-" + Path.GetFileName(path);
            // ReSharper disable AssignNullToNotNullAttribute
            string thumbnailPath = Path.Combine(thumbnailDirectory, imageFileName);
            // ReSharper restore AssignNullToNotNullAttribute
            return thumbnailPath;
        }
    }
}