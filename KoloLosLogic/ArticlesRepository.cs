using System.Data.Entity;
using System.Linq;

namespace KoloLosLogic
{
    public class ArticlesRepository
    {
        private readonly IDbSet<Article> articles;

        public ArticlesRepository(IDbSet<Article> articles)
        {
            this.articles = articles;
        }

        public Article GetById(int id)
        {
            return articles.Find(id);
        }

        public Article[] GetLatest(int count)
        {
            return GetLatest(0, count);
        }

        public Article[] GetLatest(int skip, int count)
        {
            return articles.OrderByDescending(e => e.PublishDate)
                           .Skip(skip)
                           .Take(count)
                           .ToArray();
        }

        public Article[] GetByCategory(int categoryId)
        {
            return articles.Where(a => a.Category.Id == categoryId).ToArray();
        }

        public int Count
        {
            get { return articles.Count(); }
        }
    }
}
