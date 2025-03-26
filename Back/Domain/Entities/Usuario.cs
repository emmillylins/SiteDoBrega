using Domain.Enums;

namespace Domain.Entities
{
    public class Usuario
	{
        public Usuario() {}

        public Usuario(string userName, string cpf, string nome, string? dataNasc, TipoUsuario tipoUsuario)
        {
            NomeUsuario = userName;
            Cpf = cpf;
            Nome = nome;
            DataNasc = dataNasc;
            TipoUsuario = tipoUsuario;
        }

        public int Id { get; set; }
        public string NomeUsuario { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string? DataNasc { get; set; }
        public Status? Status { get; set; } = Enums.Status.Ativo;
        public TipoUsuario TipoUsuario { get; set; }

        public List<Faixa> Faixas { get; set; } = [];
    }
}
