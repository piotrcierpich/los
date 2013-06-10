using System.Data.Entity;
using System.Web.Mvc;
using System.Linq;

using KoloLosCommon;

namespace KoloLos.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IDbSet<Article> articles;

        public HistoryController(IDbSet<Article> articles)
        {
            this.articles = articles;
        }

        public ActionResult Index()
        {
            Article history = GetHistoryArticle();
            return View(history);
        }

        private Article GetHistoryArticle()
        {
            return articles.FirstOrDefault(article => article.Category == Category.History) ?? Article.Empty;
        }
    }
}
