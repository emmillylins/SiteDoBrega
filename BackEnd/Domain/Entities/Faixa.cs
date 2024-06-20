namespace Domain.Entities
{
    public class Faixa
    {
        public Faixa() { }

        public Faixa(int id, string? titulo, string? artista, string link, int categoriaId, string nomeUsuario)
        {
            Id = id;
            Titulo = titulo;
            Artista = artista;
            Link = link;
            CategoriaId = categoriaId;
            NomeUsuario = nomeUsuario;
        }

        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Artista { get; set; }
        public string Link { get; set; }
        public int CategoriaId { get; set; }
        public string NomeUsuario { get; set; }

        public Categoria Categoria { get; set; }
        public Usuario Usuario { get; set; }
    }
}
