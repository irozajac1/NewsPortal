using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Framework.Interfaces
{
    public interface IUserManagementRepository<T> /*: IDisposable*/ where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate);

        Task<T> GetById(int id);

        bool Any(Expression<Func<T, bool>> predicate);

        void Insert(T entity);

        void Update(T entity);

        void Delete(T entity);

        IQueryable<T> IncludeAll();

        void BulkInsert(IList<T> entity);
    }
}
