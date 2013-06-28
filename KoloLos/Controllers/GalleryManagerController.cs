using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using KoloLos.Models.Gallery;
using KoloLos.Models.GalleryManager;

using KoloLosCommon;

using Omu.ValueInjecter;

namespace KoloLos.Controllers
{
    public class GalleryManagerController : Controller
    {
        private readonly IGalleriesRepository galleriesRepository;

        public GalleryManagerController(IGalleriesRepository galleriesRepository)
        {
            this.galleriesRepository = galleriesRepository;
        }

        public ActionResult Index()
        {
            IEnumerable<GalleryLink> galleryLinks = galleriesRepository.Galleries
                                                                       .OrderByDescending(g => g.PublishDate)
                                                                       .ToArray()
                                                                       .Select(g =>
                                                                                    {
                                                                                        GalleryLink galleryLink =
                                                                                                new GalleryLink();
                                                                                        galleryLink.InjectFrom(g);
                                                                                        return galleryLink;
                                                                                    });
            return View(galleryLinks);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GalleryNew galleryNew)
        {
            if (ModelState.IsValid)
            {
                GalleryFolder galleryFolder = new GalleryFolder(galleryNew.Title);
                string galleryPath = galleryFolder.CreateGalleryDirectories();


                Gallery gallery = new Gallery { Path = galleryPath };
                gallery.InjectFrom(galleryNew);
                galleriesRepository.Galleries.Add(gallery);
                galleriesRepository.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(galleryNew);
        }


        public ActionResult Edit(int id)
        {
            Gallery gallery = galleriesRepository.Galleries.Find(id);
            ImagesBrowser imagesLocation = new ImagesBrowser(gallery.Path);

            GalleryEdit galleryEdit = new GalleryEdit { FileNames = imagesLocation.GetImagesFileNames() };
            galleryEdit.InjectFrom(gallery);

            return View(galleryEdit);
        }

        public ActionResult ImageDetails(int id, string filename)
        {
            Gallery gallery = galleriesRepository.Galleries.Find(id);

            ImagesBrowser imagesBrowser = new ImagesBrowser(gallery.Path);
            string pathToImage = imagesBrowser.GetImagePath(filename);

            GalleryImageDetails galleryImageDetails = new GalleryImageDetails
                                                          {
                                                              Id = gallery.Id,
                                                              PathToImage = pathToImage,
                                                              FileName = filename
                                                          };
            return View(galleryImageDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteImage(int id, string filename)
        {
            Gallery gallery = galleriesRepository.Galleries.Find(id);

            ImageRemover imageRemover = new ImageRemover(gallery.Path);
            imageRemover.DeleteImage(filename);
            return RedirectToAction("Edit", new { id });
        }
    }
}
