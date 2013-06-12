using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

using KoloLos.Models;

using KoloLosCommon;

using Omu.ValueInjecter;

namespace KoloLos.Controllers
{
    public class NewsController : Controller
    {
        private const int AbstractsPerPageMaxCount = 10;
        private readonly IDbSet<Article> articles;

        public NewsController(IDbSet<Article> articles)
        {
            this.articles = articles;
        }

        public ActionResult Index(int pageIndex = 0)
        {
            ArticleAbstractsList newsList = new ArticleAbstractsList
                                    {
                                        NextPageExists = AnyCategoryArticlesOnNextPage(pageIndex + 1),
                                        PreviousPageExists = pageIndex > 0,
                                        PreviousPageIndex = pageIndex - 1,
                                        NextPageIndex = pageIndex + 1,
                                        ArticleAbstracts = GetAbstractsForPageIndex(pageIndex)
                                    };

            return View(newsList);
        }

        private bool AnyCategoryArticlesOnNextPage(int nextPageIndex)
        {
            return nextPageIndex * AbstractsPerPageMaxCount < articles.Count(article => article.Category == Category.News);
        }

        private ArticleAbstract[] GetAbstractsForPageIndex(int pageIndex)
        {
            IEnumerable<Article> articlesCurrentPage = GetLatest(pageIndex * AbstractsPerPageMaxCount, AbstractsPerPageMaxCount);
            return articlesCurrentPage.Select(article => {
                                                ArticleAbstract articleAbstract = new ArticleAbstract();
                                                articleAbstract.InjectFrom(article);
                                                return articleAbstract;
                                              })
                                       .ToArray();
        }

        private IEnumerable<Article> GetLatest(int skip, int count)
        {
            return articles.Where(article => article.Category == Category.News)
                           .OrderByDescending(article => article.PublishDate)
                           .Skip(skip)
                           .Take(count);
        }

        public ActionResult Details(int id)
        {
            Article article = articles.Find(id);
            ArticleDetail newsDetail = new ArticleDetail();
            newsDetail.InjectFrom(article);

            return View(newsDetail);
        }
    }
}
