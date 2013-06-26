using System;
using System.Data.Entity.Infrastructure;

namespace KoloLosCommon
{
    public interface IRepository : IDisposable 
    {
        int SaveChanges();
        DbEntityEntry Entry(object entry);
    }
}