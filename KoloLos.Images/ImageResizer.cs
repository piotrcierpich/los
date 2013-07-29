using System.Drawing;
using System.IO;
using System.Web.Helpers;

namespace KoloLos.Images
{
    public class ImageResizer
    {
        private readonly string pathToFile;

        public ImageResizer(string pathToFile)
        {
            this.pathToFile = pathToFile;
        }

        public void ResizeAndSave(Size newSize, string newPath = null)
        {
            if (string.IsNullOrEmpty(pathToFile))
                return;

            if (!File.Exists(pathToFile))
                return;

            WebImage webImage = new WebImage(pathToFile);
            webImage.Resize(newSize.Width, newSize.Height);

            webImage.Save(newPath ?? pathToFile);
        }
    }
}