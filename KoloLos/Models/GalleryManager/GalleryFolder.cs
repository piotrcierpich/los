﻿using System.IO;
using System.Linq;
using System.Web;

using KoloLos.Models.Gallery;

namespace KoloLos.Models.GalleryManager
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

            ImageThumbnail imageThumbnail = new ImageThumbnail(folderResolver.ThumbnailsDirectory);
            imageThumbnail.CreateThumbnail(pathOfImage);
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