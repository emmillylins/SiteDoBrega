using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser(){}
        public ApplicationUser(string cpf, string? dataNasc, TipoUsuario tipoUsuario)
        {
            Cpf = cpf;
            DataNasc = dataNasc;
            TipoUsuario = tipoUsuario;
        }

        public string Cpf { get; set; }
        public string? DataNasc { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public Status? Status { get; set; } = Enums.Status.Ativo;

        public List<Faixa> Faixas = [];
    }
}
