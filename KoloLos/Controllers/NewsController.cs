using System.Linq;
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
            NewsList newsList = new NewsList
                                    {
                                            NextPageExists = (pageIndex + 1) * AbstractsPerPageMaxCount < articlesRepository.Count,
                                            PreviousPageExists = pageIndex > 0,
                                            PreviousPageIndex = pageIndex - 1,
                                            NextPageIndex = pageIndex + 1,
                                            ArticleAbstracts = GetAbstractsForPageIndex(pageIndex)
                                    };

            // TODO this retrieves also main page description
            return View(newsList);
        }

        private ArticleAbstract[] GetAbstractsForPageIndex(int pageIndex)
        {
            Article[] articles = articlesRepository.GetLatest(pageIndex * AbstractsPerPageMaxCount, AbstractsPerPageMaxCount);
            return articles.Select(article => {
                                                ArticleAbstract articleAbstract = new ArticleAbstract();
                                                articleAbstract.InjectFrom(article);
                                                return articleAbstract;
                                              })
                           .ToArray();
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
