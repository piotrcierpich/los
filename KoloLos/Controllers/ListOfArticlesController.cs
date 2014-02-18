using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

using KoloLos.Models;

using KoloLosCommon;

using Omu.ValueInjecter;

namespace KoloLos.Controllers
{
    public class ListOfArticlesController : Controller
    {
        private readonly IDbSet<Article> articles;

        public ListOfArticlesController(IDbSet<Article> articles)
        {
            this.articles = articles;
        }

        public ActionResult Index(Category listName, int pageIndex = 0)
        {
            ArticleAbstractsList articleAbstracts = new ArticleAbstractsList
            {
                Category = listName,
                NextPreviousOptions = NextPreviousOptions.ForPageIndex(pageIndex, articles.Count(article => article.Category == Category.News)),
                ArticleAbstracts = GetAbstractsForPageIndex(listName, pageIndex)
            };

            return View(articleAbstracts);
        }

        private ArticleAbstract[] GetAbstractsForPageIndex(Category listName, int pageIndex)
        {
            IEnumerable<Article> articlesCurrentPage = GetLatest(listName, pageIndex * NextPreviousOptions.EntriesPerPageMaxCount, NextPreviousOptions.EntriesPerPageMaxCount);
            return articlesCurrentPage.Select(article =>
                                                        {
                                                            ArticleAbstract articleAbstract = new ArticleAbstract();
                                                            articleAbstract.InjectFrom(article);
                                                            return articleAbstract;
                                                        })
                                      .ToArray();
        }

        private IEnumerable<Article> GetLatest(Category listName, int skip, int count)
        {
            return articles.Where(article => article.Category == listName)
                           .OrderByDescending(article => article.PublishDate)
                           .Skip(skip)
                           .Take(count);
        }
    }
}
