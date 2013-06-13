﻿using System.Collections.Generic;
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
        private const int AbstractsPerPageMaxCount = 10;

        private readonly IDbSet<Article> articles;

        public ListOfArticlesController(IDbSet<Article> articles)
        {
            this.articles = articles;
        }

        public ActionResult Index(Category listName, int pageIndex)
        {
            ArticleAbstractsList articleAbstracts = new ArticleAbstractsList
            {
                Category = listName,
                NextPageExists = AnyCategoryArticlesOnNextPage(pageIndex + 1),
                PreviousPageExists = pageIndex > 0,
                PreviousPageIndex = pageIndex - 1,
                NextPageIndex = pageIndex + 1,
                ArticleAbstracts = GetAbstractsForPageIndex(listName, pageIndex)
            };

            return View(articleAbstracts);
        }

        private bool AnyCategoryArticlesOnNextPage(int nextPageIndex)
        {
            return nextPageIndex * AbstractsPerPageMaxCount < articles.Count(article => article.Category == Category.News);
        }

        private ArticleAbstract[] GetAbstractsForPageIndex(Category listName, int pageIndex)
        {
            IEnumerable<Article> articlesCurrentPage = GetLatest(listName, pageIndex * AbstractsPerPageMaxCount, AbstractsPerPageMaxCount);
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
