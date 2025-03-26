using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(Context.DataDbContext context)
        {
            _dbSet = context.Set<TEntity>();
        }

        public void Insert(TEntity obj) => _dbSet.Add(obj);
        public void Update(TEntity obj) => _dbSet.Update(obj);
        public void Delete(TEntity obj) => _dbSet.Remove(obj);
        public List<TEntity> Select() => _dbSet.AsNoTracking().ToList();
        public TEntity? Select(int id) => _dbSet.Find(id);

        public async Task InsertAsync(TEntity obj) => await _dbSet.AddAsync(obj);
        public async Task UpdateAsync(TEntity obj)
        {
            await Task.Run(() => _dbSet.Update(obj));
        }
        public async Task DeleteAsync(TEntity obj)
        {
            await Task.Run(() => _dbSet.Remove(obj));
        }
        public async Task<List<TEntity>> SelectAsync() => await _dbSet.AsNoTracking().ToListAsync();
        public async Task<TEntity> SelectAsync(int id) => await _dbSet.FindAsync(id);

    }
}
