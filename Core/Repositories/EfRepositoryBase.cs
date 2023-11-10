using Core.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Repositories
{
    public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity>
       where TEntity : Entity
       where TContext : DbContext
    {
        protected TContext Context { get; }

        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }
        public async Task<IQueryable<TEntity>> Query()
        {
            return Context.Set<TEntity>();
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return entity;
        }
        public async Task<List<TEntity>> BulkAdd(List<TEntity> entites)
        {
            Context.Entry(entites).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return entites;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
                           IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<TEntity> queryable = await Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public async Task<IPaginate<TEntity>> GetListPaginate(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool enableTracking = false)
        {
            IQueryable<TEntity> queryable = await Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
                return orderBy(queryable).ToPaginate(index, size);
            return queryable.ToPaginate(index, size);
        }

        public async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<TEntity> queryable = await Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
            {
                return await orderBy(queryable).ToListAsync();
            }
            return await queryable.ToListAsync();

        }

        public async Task<int> Count(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = false)
        {
            IQueryable<TEntity> queryable = await Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (predicate != null) queryable = queryable.Where(predicate);
            return await queryable.CountAsync();

        }

        public async Task<List<TEntity>> BulkUpdate(List<TEntity> entities)
        {
            Context.UpdateRange(entities);
            await Context.SaveChangesAsync();
            return entities;
        }

    }
}
