using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KoloLos.Images.Gallery.Manage
{
    public class GalleryFolder
    {
        private readonly FolderResolver folderResolver;
        private readonly string galleryFolder;

        public GalleryFolder(string galleryTitle)
        {
            galleryFolder = ReplaceNonCharactersOrDigitsWithUnderscore(galleryTitle);
            folderResolver = new FolderResolver(galleryFolder);
        }

        public string CreateGalleryDirectories()
        {
            CreateGalleryFoldersIfExist();

            return galleryFolder;
        }

        private string ReplaceNonCharactersOrDigitsWithUnderscore(string str)
        {
            return str.Select(c => char.IsLetterOrDigit(c) ? c : '_').Aggregate(string.Empty, (s, c) => s + c);
        }

        public void AddFile(HttpPostedFileBase file)
        {
            CreateGalleryFoldersIfExist();

            string pathOfImage = GetPathForImageFile(file.FileName);
            file.SaveAs(pathOfImage);

            IEnumerable<string> files = GetNewFilesNames(pathOfImage);

            foreach (string imageFile in files)
            {
                CreateThubnail(imageFile);
                NormalizeImageSize(imageFile);
            }
        }

        private IEnumerable<string> GetNewFilesNames(string pathOfNewFile)
        {
            GalleryFilesResolver filesResolver = new GalleryFilesResolver(pathOfNewFile);
            filesResolver.UnpackIfArchive();
            return filesResolver.GetFileNames();
        }

        private void CreateGalleryFoldersIfExist()
        {
            if (!Directory.Exists(folderResolver.ImagesDirectory))
            {
                Directory.CreateDirectory(folderResolver.ImagesDirectory);
            }

            if (!Directory.Exists(folderResolver.ThumbnailsDirectory))
            {
                Directory.CreateDirectory(folderResolver.ThumbnailsDirectory);
            }
        }

        private void CreateThubnail(string pathToImage)
        {
            ImageThumbnail imageThumbnail = new ImageThumbnail(folderResolver.ThumbnailsDirectory);
            imageThumbnail.CreateThumbnail(pathToImage);
        }

        private static void NormalizeImageSize(string pathOfImage)
        {
            ImageNormalizer imageNormalizer = new ImageNormalizer(pathOfImage);
            imageNormalizer.NormalizeImage();
        }

        private string GetPathForImageFile(string imageFile)
        {
            return Path.Combine(folderResolver.ImagesDirectory, imageFile);
        }

        public void DeleteGalleryFolders()
        {
            new DirectoryInfo(folderResolver.ImagesDirectory).Delete(true);
        }
    }
}