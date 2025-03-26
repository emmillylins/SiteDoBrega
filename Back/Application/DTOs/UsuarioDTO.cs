using Domain.Enums;
using System;

namespace Application.DTOs
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string? DataNasc { get; set; }
        public Status? Status { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
    public class CreateUsuarioDTO
    {
        public string NomeUsuario { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string? DataNasc { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
