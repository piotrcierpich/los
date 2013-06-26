using System.Data.Entity;

namespace KoloLosCommon
{
    public interface IArticlesRepository : IRepository 
    {
        DbSet<Article> Articles { get; }
    }
}
