using System.IO;
using System.Linq;

namespace KoloLos.Models.Gallery
{
    public class ImagesBrowser
    {
        private const string ImagesDirectoryPattern = "/Content/gallery/{0}/";
        private const string ThumbnailsDirectoryPattern = "/Content/gallery/{0}/thumbnails/";

        private readonly string imagesUri;
        private readonly string thumbnailsUri;
        private readonly string imagesDirectory;
        private readonly string thumbnailsDirectory;

        public ImagesBrowser(string imagesDirectoryName)
        {
            imagesUri = string.Format(ImagesDirectoryPattern, imagesDirectoryName);
            thumbnailsUri = string.Format(ThumbnailsDirectoryPattern, imagesDirectoryName);

            imagesDirectory = System.Web.HttpContext.Current.Server.MapPath(imagesUri);
            thumbnailsDirectory = System.Web.HttpContext.Current.Server.MapPath(thumbnailsUri);
        }

        public GalleryImage[] GetImages()
        {
            string[] files = GetImagesFileNames();

            return files.Where(ThumbnailExists).Select(f => new GalleryImage
                                             {
                                                 Path = imagesUri + f,
                                                 ThumbnailPath = thumbnailsUri + ImageToThumbnailName(f)
                                             })
                        .ToArray();
        }

        public string[] GetImagesFileNames()
        {
            return new DirectoryInfo(imagesDirectory).GetFiles().Select(f => f.Name).ToArray();
        }

        public string GetImagePath(string fileName)
        {
            return imagesUri + fileName;
        }

        private bool ThumbnailExists(string file)
        {
            string thumbnailFile = ImageToThumbnailName(file);
            string pathToThumbnail = Path.Combine(thumbnailsDirectory, thumbnailFile);
            return File.Exists(pathToThumbnail);
        }

        private string ImageToThumbnailName(string image)
        {
            return "thumb-" + image;
        }
    }
}