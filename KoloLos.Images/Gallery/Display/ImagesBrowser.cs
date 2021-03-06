﻿using System.IO;
using System.Linq;

namespace KoloLos.Images.Gallery.Display
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
            return Directory.Exists(folderResolver.ImagesDirectory)
                           ? new DirectoryInfo(folderResolver.ImagesDirectory).GetFiles().Select(f => f.Name).ToArray()
                           : new string[0];
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