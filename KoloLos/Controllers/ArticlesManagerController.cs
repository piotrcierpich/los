﻿using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

using KoloLos.Models.Manager;

using KoloLosCommon;

using Omu.ValueInjecter;

namespace KoloLos.Controllers
{
    public class ArticlesManagerController : Controller
    {
       private readonly ILosRepository losRepository;

       public ArticlesManagerController(ILosRepository losRepository)
        {
            this.losRepository = losRepository;
        }
        
        public ActionResult Index()
        {
            IEnumerable<ArticleTitle> articleTitles = losRepository.Articles
                                                                   .ToArray()
                                                                   .Select(a =>
                                                                            {
                                                                                ArticleTitle articleTitle = new ArticleTitle();
                                                                                articleTitle.InjectFrom(a);
                                                                                return articleTitle;
                                                                            });
            return View(articleTitles); 
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleNew articleNew)
        {
            if (ModelState.IsValid)
            {
                Article article = new Article();
                article.InjectFrom(articleNew);

                losRepository.Articles.Add(article);
                losRepository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articleNew);
        }
        
        public ActionResult Edit(int id = 0)
        {
            Article article = losRepository.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ArticleEdit articleEdit = new ArticleEdit();
            articleEdit.InjectFrom(article);
            return View(articleEdit);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArticleEdit articleEdit)
        {
            if (ModelState.IsValid)
            {
                Article article = losRepository.Articles.Find(articleEdit.Id);
                article.InjectFrom(articleEdit);
                losRepository.Entry(article).State = EntityState.Modified;
                losRepository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articleEdit);
        }
        
        public ActionResult Delete(int id = 0)
        {
            Article article = losRepository.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            ArticleEdit articleEdit = new ArticleEdit();
            articleEdit.InjectFrom(article);
            return View(articleEdit);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = losRepository.Articles.Find(id);
            losRepository.Articles.Remove(article);
            losRepository.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            losRepository.Dispose();
            base.Dispose(disposing);
        }

    }
}
