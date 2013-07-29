using System;
using System.IO;
using System.Linq;

namespace KoloLos.Images.Gallery.Manage
{
    public class ImageRemover
    {
        private readonly FolderResolver folderResolver;

        public ImageRemover(string imagesDirectoryName)
        {
            folderResolver = new FolderResolver(imagesDirectoryName);
        }

        public void DeleteImage(string filename)
        {
            try
            {
                DeleteOriginalSizeImage(filename);
                DeleteThumbnail(filename);
            } catch (InvalidOperationException)
            {
                throw new FileNotFoundException("File not found", filename);
            }
        }

        private void DeleteOriginalSizeImage(string filename)
        {
            new DirectoryInfo(folderResolver.ImagesDirectory).GetFiles()
                                                             .Single(f => StringComparer.InvariantCultureIgnoreCase.Equals(f.Name, filename))
                                                             .Delete();
        }

        private void DeleteThumbnail(string filename)
        {
            new DirectoryInfo(folderResolver.ThumbnailsDirectory).GetFiles()
                                                                 .Single(f => StringComparer.InvariantCultureIgnoreCase.Equals(f.Name, "thumb-" + filename))
                                                                 .Delete();
        }
    }
}