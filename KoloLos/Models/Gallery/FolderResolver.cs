using System.Web;

namespace KoloLos.Models.Gallery
{
    public class FolderResolver
    {
        private const string ImagesDirectoryPattern = "/Content/gallery/{0}/";
        private const string ThumbnailsDirectoryPattern = "/Content/gallery/{0}/thumbnails/";

        private readonly string thumbnailsDirectory;
        private readonly string imagesDirectory;
        private readonly string thumbnailsUri;
        private readonly string imagesUri;

        public FolderResolver(string imagesDirectoryName)
        {
            imagesUri = string.Format(ImagesDirectoryPattern, imagesDirectoryName);
            thumbnailsUri = string.Format(ThumbnailsDirectoryPattern, imagesDirectoryName);

            imagesDirectory = HttpContext.Current.Server.MapPath(imagesUri);
            thumbnailsDirectory = HttpContext.Current.Server.MapPath(thumbnailsUri);
        }

        public string ImagesUri
        {
            get { return imagesUri; }
        }

        public string ThumbnailsUri
        {
            get { return thumbnailsUri; }
        }

        public string ImagesDirectory
        {
            get { return imagesDirectory; }
        }

        public string ThumbnailsDirectory
        {
            get { return thumbnailsDirectory; }
        }
    }
}