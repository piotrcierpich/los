using System.IO;
using System.Linq;

namespace KoloLos.Models.Gallery
{
    public class ImagesBrowser
    {
        private readonly FolderResolver folderResolver;

        public ImagesBrowser(string imagesDirectoryName)
        {
            folderResolver = new FolderResolver(imagesDirectoryName);
        }

        public GalleryImage[] GetImages()
        {
            string[] files = GetImagesFileNames();

            return files.Where(ThumbnailExists).Select(f => new GalleryImage
                                             {
                                                 Path = folderResolver.ImagesUri + f,
                                                 ThumbnailPath = folderResolver.ThumbnailsUri + ImageToThumbnailName(f)
                                             })
                        .ToArray();
        }

        public string[] GetImagesFileNames()
        {
            return new DirectoryInfo(folderResolver.ImagesDirectory).GetFiles().Select(f => f.Name).ToArray();
        }

        public string GetImagePath(string fileName)
        {
            return folderResolver.ImagesUri + fileName;
        }

        private bool ThumbnailExists(string file)
        {
            string thumbnailFile = ImageToThumbnailName(file);
            string pathToThumbnail = Path.Combine(folderResolver.ThumbnailsDirectory, thumbnailFile);
            return File.Exists(pathToThumbnail);
        }

        private string ImageToThumbnailName(string image)
        {
            return "thumb-" + image;
        }
    }
}