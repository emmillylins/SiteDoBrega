using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(TipoUsuario tipoUsuario)
        {
            TipoUsuario = tipoUsuario;
        }

        public TipoUsuario TipoUsuario { get; set; }
    }
}
