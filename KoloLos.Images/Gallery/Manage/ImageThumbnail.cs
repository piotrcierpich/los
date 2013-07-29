using System.Drawing;
using System.IO;

namespace KoloLos.Images.Gallery.Manage
{
    public class ImageThumbnail
    {
        private static readonly Size ThumbnailSize = new Size(200, 200);
        private readonly string thumbnailDirectory;

        public ImageThumbnail(string thumbnailDirectory)
        {
            this.thumbnailDirectory = thumbnailDirectory;
        }

        public void CreateThumbnail(string path)
        {
            ImageResizer imageResizer = new ImageResizer(path);
            var thumbnailPath = GetThumbnailPath(path);
            imageResizer.ResizeAndSave(ThumbnailSize, thumbnailPath);
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