using NewsPortal.Framework.Data.Context;
using NewsPortal.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Framework.Repository
{
    public class NewsPortalUnitOfWork : INewsPortalUnitOfWork
    {
        private readonly NewsPortalContext _context;

        public NewsPortalUnitOfWork(NewsPortalContext context)
        {
            _context = context;
        }

        public INewsPortalGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new NewsPortalGenericRepository<TEntity>(_context);
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
