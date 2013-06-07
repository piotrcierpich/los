using System.Linq;
using System.Web.Mvc;

using KoloLos.Models;

using KoloLosLogic;

using Omu.ValueInjecter;

namespace KoloLos.Controllers
{
    public class HomeController : Controller
    {
        private const string MainPageCategoryName = "Main page";
        private const int LatestArticlesCount = 3;

        private readonly ArticlesRepository articlesRepository;
        private readonly CategoryRepository categoryRepository;

        public HomeController(ArticlesRepository articlesRepository, CategoryRepository categoryRepository)
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

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
