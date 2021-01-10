using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Framework.Interfaces
{
    public interface INewsPortalGenericRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAll();

        //IQueryable<T> GetAllSieve();

        Task<int> CountAll();

        Task<int> CountWhere(Expression<Func<T, bool>> predicate);

        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate);

        Task<T> GetById(Guid id);
        Task<T> GetByIntId(int id);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        IQueryable<T> IncludeAll();

        void BulkInsert(IList<T> entity);
    }
}
