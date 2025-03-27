namespace Application.DTOs
{
    public class FaixaDTO
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Artista { get; set; }
        public string Link { get; set; }
        public int CategoriaId { get; set; }
        public string UsuarioId { get; set; }
    }
    public class CreateFaixaDTO
    {
        public string? Titulo { get; set; }
        public string? Artista { get; set; }
        public string Link { get; set; }
        public int CategoriaId { get; set; }
        public string UsuarioId { get; set; }
    }
}
