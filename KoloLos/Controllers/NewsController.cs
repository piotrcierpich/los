﻿using System.Linq;
using System.Web.Mvc;

using KoloLos.Models;

using KoloLosLogic;

using Omu.ValueInjecter;

namespace KoloLos.Controllers
{
    public class NewsController : Controller
    {
        private const int AbstractsPerPageMaxCount = 10;
        private readonly ArticlesRepository articlesRepository;

        public NewsController(ArticlesRepository articlesRepository)
        {
            this.articlesRepository = articlesRepository;
        }

        public ActionResult Index(int pageIndex = 0)
        {
            if (pageIndex == 0)
            {
                ViewBag.NextPageExists = (pageIndex + 1) * AbstractsPerPageMaxCount < articlesRepository.Count;
                ViewBag.PreviousPageExists = false;
                ViewBag.NextPageIndex = 1;
            }
            else
            {
                ViewBag.NextPageExists = (pageIndex + 1) * AbstractsPerPageMaxCount < articlesRepository.Count;
                ViewBag.PreviousPageExists = true;
                ViewBag.PreviousPageIndex = pageIndex - 1;
                ViewBag.NextPageIndex = pageIndex + 1;
            }

            // TODO this retrieves also main page description

            Article[] articles = articlesRepository.GetLatest(pageIndex * AbstractsPerPageMaxCount, AbstractsPerPageMaxCount);
            ArticleAbstract[] abstracts = articles.Select(article =>
                                                       {
                                                           ArticleAbstract articleAbstract = new ArticleAbstract();
                                                           articleAbstract.InjectFrom(article);
                                                           return articleAbstract;
                                                       })
                                                 .ToArray();
            return View(abstracts);
        }

        public ActionResult Details(int id)
        {
            Article article = articlesRepository.GetById(id);
            NewsDetail newsDetail = new NewsDetail();
            newsDetail.InjectFrom(article);

            return View(newsDetail);
        }

        //
        // GET: /News/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /News/Create

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
        // GET: /News/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /News/Edit/5

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
        // GET: /News/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /News/Delete/5

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