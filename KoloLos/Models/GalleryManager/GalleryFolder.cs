using System.IO;
using System.Linq;
using System.Web;

namespace KoloLos.Models.GalleryManager
{
    public class GalleryFolder
    {
        private const string ImagesDirectoryPattern = "/Content/gallery/{0}/";
        private const string ThumbnailsDirectoryPattern = "/Content/gallery/{0}/thumbnails/";

        private readonly string imagesDirectory;
        private readonly string thumbnailsDirectory;
        private readonly string galleryFolder;

        public GalleryFolder(string galleryTitle)
        {
            galleryFolder = ReplaceNonCharactersOrDigitsWithUnderscore(galleryTitle);
            string imagesUri = string.Format(ImagesDirectoryPattern, galleryFolder);
            string thumbnailsUri = string.Format(ThumbnailsDirectoryPattern, galleryFolder);

            imagesDirectory = HttpContext.Current.Server.MapPath(imagesUri);
            thumbnailsDirectory = HttpContext.Current.Server.MapPath(thumbnailsUri);
        }

        public string CreateGalleryDirectories()
        {
            new DirectoryInfo(imagesDirectory).Create();
            new DirectoryInfo(thumbnailsDirectory).Create();

            return galleryFolder;
        }

        private string ReplaceNonCharactersOrDigitsWithUnderscore(string str)
        {
            return str.Select(c => char.IsLetterOrDigit(c) ? c : '_').Aggregate(string.Empty, (s, c) => s + c);
        }
    }
}