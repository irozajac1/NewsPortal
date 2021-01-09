
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Framework.Context;
using UserManagement.Framework.Interfaces;

namespace UserManagement.Framework.Repositories
{
    public class UserManagementRepository<T> : IUserManagementRepository<T> where T : class
    {
        private readonly UserManagementContext _context;
        private readonly DbSet<T> _dbSet;

        public UserManagementRepository(UserManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
            => await _dbSet.ToListAsync();

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate) => await _dbSet.FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();

        public async Task<T> GetById(int id)
            => await _dbSet.FindAsync(id);

        public bool Any(Expression<Func<T, bool>> predicate) => _dbSet.Any(predicate);

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

    }
}
    
