namespace Infrastructure.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(TEntity id);
        List<TEntity> Select();
        TEntity Select(int id);

        Task InsertAsync(TEntity obj);
        Task UpdateAsync(TEntity obj);
        Task DeleteAsync(TEntity obj);
        Task<List<TEntity>> SelectAsync();
        Task<TEntity> SelectAsync(int id);
    }
}
