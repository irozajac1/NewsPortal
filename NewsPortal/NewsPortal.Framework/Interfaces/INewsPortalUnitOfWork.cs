using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Framework.Interfaces
{
    public interface INewsPortalUnitOfWork
    {
        INewsPortalGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        Task SaveChangesAsync();

        Task<int> SaveChanges();
    }
}
