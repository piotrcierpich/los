using System;
using System.IO;
using System.Linq;

namespace KoloLos.Models.GalleryManager
{
    public class ImageRemover
    {
        private const string ImagesDirectoryPattern = "/Content/gallery/{0}/";
        private const string ThumbnailsDirectoryPattern = "/Content/gallery/{0}/thumbnails/";

        private readonly string imagesUri;
        private readonly string thumbnailsUri;
        private readonly string imagesDirectory;
        private readonly string thumbnailsDirectory;

        public ImageRemover(string imagesDirectoryName)
        {
            imagesUri = string.Format(ImagesDirectoryPattern, imagesDirectoryName);
            thumbnailsUri = string.Format(ThumbnailsDirectoryPattern, imagesDirectoryName);

            imagesDirectory = System.Web.HttpContext.Current.Server.MapPath(imagesUri);
            thumbnailsDirectory = System.Web.HttpContext.Current.Server.MapPath(thumbnailsUri);
        }

        public void DeleteImage(string filename)
        {
            try
            {
                DeleteOriginalSizeImage(filename);
                DeleteThumbnail(filename);
            }
            catch (InvalidOperationException)
            {
                throw new FileNotFoundException("File not found", filename);
            }
        }

        private void DeleteOriginalSizeImage(string filename)
        {
            new DirectoryInfo(imagesDirectory).GetFiles()
                                              .Single(f => StringComparer.InvariantCultureIgnoreCase.Equals(f.Name, filename))
                                              .Delete();

        }

        private void DeleteThumbnail(string filename)
        {
            new DirectoryInfo(thumbnailsDirectory).GetFiles()
                                                  .Single(f => StringComparer.InvariantCultureIgnoreCase.Equals(f.Name, "thumb-" + filename))
                                                  .Delete();
        }
    }
}