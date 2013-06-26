using System.Data.Entity;

using KoloLosCommon;

namespace KoloLosDataLayer
{
    public class ArticlesDataContext : DbContext, IArticlesRepository, IGalleriesRepository
    {
        public ArticlesDataContext()
            : base("KoloLos")
        { }

        static ArticlesDataContext()
        {
            Database.SetInitializer(new LosDataInitializer());
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
    }
}
