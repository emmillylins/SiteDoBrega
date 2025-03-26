namespace Domain.Entities
{
    public class Faixa
    {
        public Faixa() { }

        public Faixa(string? titulo, string? artista, string link, int categoriaId, int usuarioId)
        {
            Titulo = titulo;
            Artista = artista;
            Link = link;
            CategoriaId = categoriaId;
            UsuarioId = usuarioId;
        }

        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Artista { get; set; }
        public string Link { get; set; }
        public int CategoriaId { get; set; }
        public int UsuarioId { get; set; }

        public Categoria Categoria { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}
