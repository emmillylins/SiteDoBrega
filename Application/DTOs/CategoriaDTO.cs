namespace Application.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public string Url { get; set; }
        public string Img { get; set; }
        public bool? Ativo { get; set; }
    }
    public class CreateCategoriaDTO
    {
        public string Desc { get; set; }
        public string Url { get; set; }
        public string Img { get; set; }
        public bool? Ativo { get; set; }

        public List<FaixaDTO> Faixas { get; set; } = [];
    }
}
