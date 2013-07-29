using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace KoloLos.Images.Slides
{
    public class Slideshow
    {
        private readonly Size slideSize = new Size(250, 188);

        private const string SlideshowUri = "/content/gallery/slideshow/";
        private readonly string slideshowDirectory;

        public Slideshow()
        {
            slideshowDirectory = HttpContext.Current.Server.MapPath(SlideshowUri);
        }

        public void SetImagesForSlideshow(string imagesDirectory)
        {
            CreateSlideshowDirectoryIfNecessary();
            DeleteAllFromSlideshowDirectory();
            ResizeAndCopyToSlideshowDirectory(imagesDirectory);
        }

        private void CreateSlideshowDirectoryIfNecessary()
        {
            if (!Directory.Exists(slideshowDirectory))
                Directory.CreateDirectory(slideshowDirectory);
        }

        private void DeleteAllFromSlideshowDirectory()
        {
            string[] slideshowfiles = Directory.GetFiles(slideshowDirectory);
            foreach (string slideshowfile in slideshowfiles)
            {
                File.Delete(slideshowfile);
            }
        }

        private void ResizeAndCopyToSlideshowDirectory(string imagesDirectory)
        {
            string[] images = Directory.GetFiles(imagesDirectory);
            foreach (string image in images)
            {
                ImageResizer imageResizer = new ImageResizer(image);
                string pathToSlide = GetPathToSlideFromImage(image);
                imageResizer.ResizeAndSave(slideSize, pathToSlide);
            }
        }

        private string GetPathToSlideFromImage(string image)
        {
            string filename = Path.GetFileName(image);
            // ReSharper disable AssignNullToNotNullAttribute
            return Path.Combine(slideshowDirectory, filename);
            // ReSharper restore AssignNullToNotNullAttribute
        }

        public string[] GetSlidesUris()
        {
            CreateSlideshowDirectoryIfNecessary();
            string[] files = Directory.GetFiles(slideshowDirectory).Select(Path.GetFileName).ToArray();
            return files.Select(f => SlideshowUri + f).ToArray();
        }
    }
}