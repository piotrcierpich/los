using System.Data.Entity;

namespace KoloLosCommon
{
    public interface IGalleriesRepository : IRepository
    {
        DbSet<Gallery> Galleries { get; }
    }
}