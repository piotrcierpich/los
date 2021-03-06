﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KoloLos.Images.Gallery.Display;
using KoloLos.Images.Gallery.Manage;
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

        public ActionResult Delete(int id)
        {
            Gallery galleryToDelete = galleriesRepository.Galleries.Find(id);
            GalleryDelete galleryDelete = new GalleryDelete();
            galleryDelete.InjectFrom(galleryToDelete);
            return View(galleryDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGallery(int id)
        {
            if (ModelState.IsValid)
            {
                Gallery galleryToDelete = galleriesRepository.Galleries.Find(id);
                galleriesRepository.Galleries.Remove(galleryToDelete);
                galleriesRepository.SaveChanges();

                try
                {
                    GalleryFolder galleryFolder = new GalleryFolder(galleryToDelete.Path);
                    galleryFolder.DeleteGalleryFolders();
                }
                catch
                {
                }

                return RedirectToAction("Index");
            }

            return View("Index");
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

        public ActionResult AddImage(int id)
        {
            return View(id);
        }

        [HttpPost]
        public ActionResult AddImage(int id, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // TODO add thumbnail generation - probably a separate class, after
                Gallery gallery = galleriesRepository.Galleries.Find(id);
                GalleryFolder galleryFolder = new GalleryFolder(gallery.Path);
                galleryFolder.AddFile(file);
            }

            return RedirectToAction("Edit", new { id });
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
