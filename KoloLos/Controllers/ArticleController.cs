using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

using KoloLos.Models;

using KoloLosCommon;

using Omu.ValueInjecter;

namespace KoloLos.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IDbSet<Article> articles;

        public ArticleController(IDbSet<Article> articles)
        {
            this.articles = articles;
        }

        public ActionResult ByCategory(Category category)
        {
            Article article = articles.First(a => a.Category == category);

            ArticleDetail articleDetail = new ArticleDetail();
            articleDetail.InjectFrom(article);

            return View("Index", articleDetail);
        }

        public ActionResult ById(int id)
        {
            Article article = articles.Find(id);

            ArticleDetail articleDetail = new ArticleDetail();
            articleDetail.InjectFrom(article);

            return View("Index", articleDetail);
        }
    }
}
