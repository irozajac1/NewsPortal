using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Framework.Context;
using UserManagement.Framework.Interfaces;

namespace UserManagement.Framework.Repositories
{
    public class UserManagementUnitOfWork : IUserManagementUnitOfWork//, /*IDisposable*/
    {
        private readonly UserManagementContext _context;

        public UserManagementUnitOfWork(UserManagementContext context)
        {
            _context = context;
        }

        public IUserManagementRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new UserManagementRepository<TEntity>(_context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}

