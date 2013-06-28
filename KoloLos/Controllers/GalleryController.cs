using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KoloLos.Models;
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

        public ActionResult Index(int pageIndex = 0)
        {
            GalleryLink[] galleryLinks = GetGalleryLinks(pageIndex);

            Galleries galleriesModel = new Galleries
                                      {
                                          Category = Category.Gallery,
                                          Gallery = galleryLinks,
                                          NextPreviousOptions = NextPreviousOptions.ForPageIndex(pageIndex,galleries.Count())
                                      };
            
            return View(galleriesModel);
        }

        private GalleryLink[] GetGalleryLinks(int pageIndex)
        {
            IEnumerable<Gallery> latestGalleries = GetLatest(pageIndex * NextPreviousOptions.EntriesPerPageMaxCount, NextPreviousOptions.EntriesPerPageMaxCount);
            return latestGalleries.Select(g =>
                                            {
                                                GalleryLink galleryLink = new GalleryLink();
                                                galleryLink.InjectFrom(g);
                                                return galleryLink;
                                            }).ToArray();
        }

        private IEnumerable<Gallery> GetLatest(int skip, int count)
        {
            return galleries.OrderByDescending(g => g.PublishDate).Skip(skip).Take(count);
        }

        public ActionResult Details(int id)
        {
            Gallery gallery = galleries.Find(id);
            ImagesBrowser imagesLocation = new ImagesBrowser(gallery.Path);
            Images images = new Images { Title = gallery.Title, GalleryImages = imagesLocation.GetImages() };
            return View(images);
        }
    }
}
