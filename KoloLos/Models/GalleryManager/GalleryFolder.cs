using System.IO;
using System.Linq;

using KoloLos.Models.Gallery;

namespace KoloLos.Models.GalleryManager
{
    public class GalleryFolder
    {
        private readonly FolderResolver folderResolver ;
        private readonly string galleryFolder;

        public GalleryFolder(string galleryTitle)
        {
            galleryFolder = ReplaceNonCharactersOrDigitsWithUnderscore(galleryTitle);
            folderResolver = new FolderResolver(galleryFolder);
        }

        public string CreateGalleryDirectories()
        {
            new DirectoryInfo(folderResolver.ImagesDirectory).Create();
            new DirectoryInfo(folderResolver.ThumbnailsDirectory).Create();

            return galleryFolder;
        }

        private string ReplaceNonCharactersOrDigitsWithUnderscore(string str)
        {
            return str.Select(c => char.IsLetterOrDigit(c) ? c : '_').Aggregate(string.Empty, (s, c) => s + c);
        }

        public string GetPathForImageFile(string imageFile)
        {
            return Path.Combine(folderResolver.ImagesDirectory, imageFile);
        }
    }
}