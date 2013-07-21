using System.Drawing;
using System.IO;
using System.Web;

using KoloLos.Models.GalleryManager;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace KoloLos.Tests.Models.GalleryManager
{
    [TestClass]
    public class ImageThumbnailTests
    {

        [DeploymentItem("TestDeploy\\Tulips.jpg")]
        [TestMethod]
        public void ShouldCreateThumbnailImageFileWhenRequested()
        {
            const string thumbnailDirectory = "thunbmails";
            Directory.CreateDirectory(thumbnailDirectory);
            string fullPathToDirectory = Path.GetFullPath(thumbnailDirectory);
            ImageThumbnail imageThumbnail = new ImageThumbnail(fullPathToDirectory);
            imageThumbnail.CreateThumbnail("Tulips.jpg");

            Bitmap bitmap = new Bitmap(Path.Combine(thumbnailDirectory, "thumb-tulips.jpg"));
            Assert.AreEqual(ImageThumbnail.ThumbnailHeight, bitmap.Size.Height);
            Assert.AreEqual(ImageThumbnail.ThumbnailWidth, bitmap.Size.Width);
        }
    }
}
