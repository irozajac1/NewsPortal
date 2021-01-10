using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using NewsPortal.Framework.Data.Context;
using NewsPortal.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Framework.Repository
{
    public class NewsPortalGenericRepository<T> : INewsPortalGenericRepository<T> where T : class
    {
        private readonly NewsPortalContext _context;
        private readonly DbSet<T> _dbSet;

        public NewsPortalGenericRepository(NewsPortalContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
            => await _dbSet.ToListAsync();

        public IQueryable<T> GetAllSieve() => _dbSet.AsNoTracking(); //to be deleted

        public async Task<int> CountAll() => await _dbSet.CountAsync();

        public async Task<int> CountWhere(Expression<Func<T, bool>> predicate) => await _dbSet.CountAsync(predicate);

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate) => await _dbSet.FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();


        public async Task<T> GetById(Guid id)
            => await _dbSet.FindAsync(id);

        public async Task<T> GetByIntId(int id) => await _dbSet.FindAsync(id);

        public void Insert(T entity)
            => _dbSet.Add(entity);

        public void BulkInsert(IList<T> entity)
        {
            _context.BulkInsert(entity, new BulkConfig { PreserveInsertOrder = true, SetOutputIdentity = true });
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual IQueryable<T> IncludeAll()
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var property in _context.Model.FindEntityType(typeof(T)).GetNavigations())
                query = query.Include(property.Name);
            return query;
        }

        public void Delete(T entity)
            => _dbSet.Remove(entity);

        #region IDisposable Support

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _context.Dispose();
            }

            disposed = true;
        }

        #endregion IDisposable Support
    }
}

