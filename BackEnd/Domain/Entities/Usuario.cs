namespace Domain.Entities
{
    public class Usuario
    {
        public Usuario() { }
        public Usuario(string nomeUsuario, string cpf, string nome, string? dataNasc)
        {
            NomeUsuario = nomeUsuario;
            Cpf = cpf;
            Nome = nome;
            DataNasc = dataNasc;
        }

        public string NomeUsuario { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string? DataNasc { get; set; }
        public bool Ativo { get; set; } = true;

        public List<Faixa> Faixas { get; set; } = new List<Faixa>();
    }
}
