using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoloLosCommon
{
    public interface ILosRepository : IDisposable
    {
        int SaveChanges();
        DbEntityEntry Entry(object entry);

        DbSet<Article> Articles { get; }
    }
}
