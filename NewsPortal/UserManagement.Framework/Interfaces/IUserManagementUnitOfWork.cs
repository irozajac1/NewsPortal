using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Framework.Interfaces
{
    public interface IUserManagementUnitOfWork
    {
        IUserManagementRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        Task SaveChangesAsync();

        Task<int> SaveChanges();
    }
}
