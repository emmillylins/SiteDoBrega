using Infrastructure.Context;

namespace Infrastructure.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InfraDbContext _dbContext;
        public UnitOfWork(InfraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
