using Domain.Entities;
using Infrastructure.Base;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        private readonly DbSet<Usuario> _dbSetUsuario;

        public UsuarioRepository(InfraDbContext context) : base(context)
        {
            _dbSetUsuario = context.Set<Usuario>();
        }
    }
}
