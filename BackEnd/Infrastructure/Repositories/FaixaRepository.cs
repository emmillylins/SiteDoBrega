using Domain.Entities;
using Infrastructure.Base;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FaixaRepository : BaseRepository<Faixa>, IFaixaRepository
    {
        private readonly DbSet<Categoria> _dbSetCategorias;
        private readonly DbSet<Usuario> _dbSetUsuarios;

        public FaixaRepository(InfraDbContext context) : base(context)
        {
            _dbSetCategorias = context.Set<Categoria>();
            _dbSetUsuarios = context.Set<Usuario>();
        }

        public List<Categoria> GetCategorias() => _dbSetCategorias.ToList();
        public List<Usuario> GetUsuarios() => _dbSetUsuarios.ToList();
    }
}
