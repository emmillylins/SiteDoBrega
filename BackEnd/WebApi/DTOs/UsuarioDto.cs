namespace WebApi.DTOs
{
    public class UsuarioDto
    {
        public string NomeUsuario { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string? DataNasc { get; set; }
        public bool Ativo { get; set; } = true;

        public List<FaixaDto>? Faixas { get; set; }
    }
    public class CreateUsuarioDto
    {
        public string NomeUsuario { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string? DataNasc { get; set; }
        public bool Ativo { get; set; } = true;

        public List<CreateFaixaDto>? Faixas { get; set; }
    }
}
