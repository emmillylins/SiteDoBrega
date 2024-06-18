namespace Infrastructure.Base
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();
    }
}
