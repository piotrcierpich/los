using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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

        //
        // GET: /Manager/

        public ActionResult Index()
        {
            return View(losRepository.Articles.ToList());
        }

        //
        // GET: /Manager/Details/5

        public ActionResult Details(int id = 0)
        {
            Article article = losRepository.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // GET: /Manager/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Manager/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                losRepository.Articles.Add(article);
                losRepository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        //
        // GET: /Manager/Edit/5

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

        //
        // POST: /Manager/Edit/5

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

        //
        // GET: /Manager/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Article article = losRepository.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // POST: /Manager/Delete/5

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
