namespace WebApi.DTOs
{
    public class FaixaDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Artista { get; set; }
        public string Link { get; set; }
        public int CategoriaId { get; set; }
        public string NomeUsuario { get; set; }

        public UsuarioDto Usuario { get; set; }
        public CategoriaDto Categoria { get; set; }
    }
    public class CreateFaixaDto
    {
        public string? Titulo { get; set; }
        public string? Artista { get; set; }
        public string Link { get; set; }
        public int CategoriaId { get; set; }
        public string NomeUsuario { get; set; }
    }
}
