using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

using KoloLos.Models;

using KoloLosCommon;

using Omu.ValueInjecter;

namespace KoloLos.Controllers
{
    public class MainController : Controller
    {
        private const int LatestArticlesCount = 3;

        private readonly IDbSet<Article> articles;

        public MainController(IDbSet<Article> articles)
        {
            this.articles = articles;
        }

        public ActionResult Index()
        {
            Article mainPageArticle = GetMainPageArticle();

            MainPageModel mainPageModel = new MainPageModel
            {
                DescriptionTitle = mainPageArticle.Title,
                Description = mainPageArticle.Content,
                Abstracts = GetLatestAbstracts()
            };
            return View(mainPageModel);
        }

        private Article GetMainPageArticle()
        {
            return articles.First(article => article.Category == Category.Main);
        }

        private ArticleAbstract[] GetLatestAbstracts()
        {
            IEnumerable<Article> latestArticles = GetLatestNewsArticles();
            ArticleAbstract[] abstracts = latestArticles.Select(article =>
                                                            {
                                                                var articleAbstract = new ArticleAbstract();
                                                                articleAbstract.InjectFrom(article);
                                                                return articleAbstract;
                                                            })
                                                  .ToArray();

            return abstracts;
        }

        private IEnumerable<Article> GetLatestNewsArticles()
        {
            return articles.Where(article => article.Category == Category.News)
                           .OrderByDescending(article => article.PublishDate)
                           .Take(LatestArticlesCount);
        }
    }
}
