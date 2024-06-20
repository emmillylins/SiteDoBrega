using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(int id);
        IList<TEntity> Select();
        TEntity Select(int id);

        void Delete(string id);
        TEntity Select(string id);

        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);
        Task<List<TEntity>> GetDataAsync(Expression<Func<TEntity, bool>> expression = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int? skip = null, int? take = null);
    }
}
