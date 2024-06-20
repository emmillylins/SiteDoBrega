using Domain.Entities;
using Infrastructure.Base;
using Infrastructure.Context;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(InfraDbContext context) : base(context)
        {
        }
    }
}
