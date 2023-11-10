using Core.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Repositories
{
    public interface IRepository<T> : IQuery<T> where T : Entity
    {
        Task<T> Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,
              IIncludableQueryable<T, object>>? include = null, bool enableTracking = false);
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>>? predicate = null,
                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                       Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                       bool enableTracking = false);

        Task<IPaginate<T>> GetListPaginate(Expression<Func<T, bool>>? predicate = null,
                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                       Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                       int index = 0, int size = 10,
                       bool enableTracking = false);
        Task<T> Add(T entity);
        Task<List<T>> BulkAdd(List<T> entites);
        Task<T> Update(T entity);
        Task<List<T>> BulkUpdate(List<T> entities);
        Task<T> Delete(T entity);
        Task<int> Count(Expression<Func<T, bool>>? predicate = null, bool enableTracking = false);
    }
}
