using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        //
        // GET: /GalleryManager/

        public ActionResult Index()
        {
            IEnumerable<GalleryLink> galleryLinks = galleriesRepository.Galleries.ToArray().Select(g =>
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
    }
}
