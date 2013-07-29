using System.Linq;
using System.Web.Mvc;

using KoloLos.Images.Gallery;
using KoloLos.Models;

using KoloLosCommon;

using Omu.ValueInjecter;

namespace KoloLos.Controllers
{
    public class SlideShowManagerController : Controller
    {
        private readonly IGalleriesRepository galleriesRepository;

        public SlideShowManagerController(IGalleriesRepository galleriesRepository)
        {
            this.galleriesRepository = galleriesRepository;
        }

        public ActionResult Index()
        {
            AvailableGallery[] availableGalleries = galleriesRepository.Galleries
                                                                       .OrderByDescending(gallery => gallery.PublishDate)
                                                                       .ToArray()
                                                                       .Select(gallery =>
                                                                                        {
                                                                                            AvailableGallery availableGallery = new AvailableGallery();
                                                                                            availableGallery.InjectFrom(gallery);
                                                                                            return availableGallery;
                                                                                        })
                                                                       .ToArray();

            GalleryForSlideshow galleryForSlideshow = new GalleryForSlideshow { AvailableGalleries = availableGalleries };
            return View(galleryForSlideshow);
        }

        [HttpPost]
        public ActionResult Index(GalleryForSlideshow galleryForSlideshow)
        {
            if (ModelState.IsValid)
            {
                Gallery gallery = galleriesRepository.Galleries.Find(galleryForSlideshow.SelectedId);

                FolderResolver folderResolver = new FolderResolver(gallery.Path);

                Slideshow slideshow = new Slideshow();
                slideshow.SetImagesForSlideshow(folderResolver.ImagesDirectory);

                return RedirectToAction("Index", "Manager");
            }

            return View("Index");
        }
    }
}
