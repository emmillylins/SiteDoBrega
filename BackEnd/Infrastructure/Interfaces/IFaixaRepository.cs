using Domain.Entities;
using Infrastructure.Base;

namespace Infrastructure.Interfaces
{
    public interface IFaixaRepository : IBaseRepository<Faixa>
    {
        public List<Categoria> GetCategorias();
        public List<Usuario> GetUsuarios();
    }
}
