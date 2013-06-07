using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KoloLos.Models;

using KoloLosLogic;

using Omu.ValueInjecter;

namespace KoloLos.Controllers
{
    public class MainController : Controller
    {
        private const string MainPageCategoryName = "Main page";
        private const int LatestArticlesCount = 3;

        private readonly ArticlesRepository articlesRepository;
        private readonly CategoryRepository categoryRepository;

        public MainController(ArticlesRepository articlesRepository, CategoryRepository categoryRepository)
        {
            this.articlesRepository = articlesRepository;
            this.categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            Category mainPageCategory = categoryRepository.GetByNameOrNull(MainPageCategoryName);
            Article mainPageArticle = articlesRepository.GetByCategory(mainPageCategory.Id).First();

            HomeModel homeModel = new HomeModel
            {
                DescriptionTitle = mainPageArticle.Title,
                Description = mainPageArticle.Content,
                Abstracts = GetLatestAbstracts()
            };
            return View(homeModel);
        }

        private ArticleAbstract[] GetLatestAbstracts()
        {
            Article[] articles = articlesRepository.GetLatest(LatestArticlesCount);
            ArticleAbstract[] abstracts = articles.Select(article =>
            {
                var articleAbstract = new ArticleAbstract();
                articleAbstract.InjectFrom(article);
                return articleAbstract;
            })
            .ToArray();

            return abstracts;
        }

        //
        // GET: /Main/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Main/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Main/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Main/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Main/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Main/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Main/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
