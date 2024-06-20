using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(InfraDbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public void Insert(TEntity obj) => _dbSet.Add(obj);
        public void Update(TEntity obj) => _dbSet.Update(obj);
        public void Delete(int id) => _dbSet.Remove(Select(id));
        public IList<TEntity> Select() => _dbSet.ToList();
        public TEntity Select(int id) => _dbSet.Find(id);

        public virtual void Delete(string id) => _dbSet.Remove(Select(id));
        public virtual TEntity Select(string id) => _dbSet.Find(id);


        public async Task<int> CountAsync(Expression<System.Func<TEntity, bool>> expression)
        {
            return await _dbSet.CountAsync(expression);
        }

        public async Task<TEntity> FirstAsync(Expression<System.Func<TEntity, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<System.Collections.Generic.List<TEntity>> GetDataAsync(
            Expression<System.Func<TEntity, bool>> expression = null,
            Func<System.Linq.IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int? skip = null,
            int? take = null)
        {
            var query = _dbSet.AsQueryable();

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (include != null)
            {
                query = include(query);
            }

            if (skip != null && skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null && take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return await query.ToListAsync();
        }
    }
}
