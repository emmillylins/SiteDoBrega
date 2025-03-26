using Domain.Enums;

namespace Application.DTOs
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        
        public string NomeUsuario { get; set; }
        public string Cpf { get; set; }
        public string? Nome { get; set; }
        public string? DataNasc { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
    public class CreateApplicationUserDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        
        public string NomeUsuario { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string? DataNasc { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
