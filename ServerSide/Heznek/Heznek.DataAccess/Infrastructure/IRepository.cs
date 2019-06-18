using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.DataAccess.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Set { get; }

        T Insert(T entity);

        Task<T> InsertAsync(T entity);

        IEnumerable<T> InsertRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> InsertRangeAsync(IEnumerable<T> entities);

        T Update(T entity);

        Task<T> UpdateAsync(T Entity);

        IEnumerable<T> UpdateRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entities);

        void Delete(T entity, bool acceptNull = false);

        void DeleteRange(IEnumerable<T> entities);

        IQueryable<T> Include(params Expression<Func<T, object>>[] include);
    }
}