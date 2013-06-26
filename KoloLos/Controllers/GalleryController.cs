using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KoloLos.Models.Gallery;

using KoloLosCommon;

using Omu.ValueInjecter;

namespace KoloLos.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IDbSet<Gallery> galleries;

        public GalleryController(IDbSet<Gallery> galleries)
        {
            this.galleries = galleries;
        }

        public ActionResult Index()
        {
            GalleryLink[] galleryLinks = galleries.ToArray().Select(g =>
                                                             {
                                                                 GalleryLink galleryLink = new GalleryLink();
                                                                 galleryLink.InjectFrom(g);
                                                                 return galleryLink;
                                                             }).ToArray();
            Galleries galleriesModel = new Galleries
                                      {
                                          Category = Category.Gallery,
                                          Gallery = galleryLinks,
                                          NextPageExists = true,
                                          NextPageIndex = 1,
                                          PreviousPageExists = true,
                                          PreviousPageIndex = 0
                                      };
            
            return View(galleriesModel);
        }

        public ActionResult Details(int id)
        {
            Gallery gallery = galleries.Find(id);
            ImagesLocation imagesLocation = new ImagesLocation(gallery.Path);
            Images images = new Images { Title = gallery.Title, GalleryImages = imagesLocation.GetImages() };
            return View(images);
        }
    }
}
