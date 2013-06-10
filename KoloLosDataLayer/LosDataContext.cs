using System.Data.Entity;

using KoloLosCommon;

namespace KoloLosDataLayer
{
    public class LosDataContext : DbContext
    {
        public LosDataContext()
            : base("KoloLos")
        { }

        static LosDataContext()
        {
            Database.SetInitializer(new LosDataInitializer());
        }

        public DbSet<Article> Articles { get; set; }
    }
}
