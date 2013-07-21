using System.IO;
using System.Web;
using System.Web.Helpers;

namespace KoloLos.Models.GalleryManager
{
    public class ImageThumbnail
    {
        private readonly string thumbnailDirectory;

        public const int ThumbnailHeight = 100;
        public const int ThumbnailWidth = 100;

        public ImageThumbnail(string thumbnailDirectory)
        {
            this.thumbnailDirectory = thumbnailDirectory;
        }

        public void CreateThumbnail(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            if (!File.Exists(path))
                return;

            WebImage webImage = new WebImage(path);
            webImage.Resize(ThumbnailWidth, ThumbnailHeight);

            var thumbnailPath = GetThumbnailPath(path);
            webImage.Save(thumbnailPath);
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