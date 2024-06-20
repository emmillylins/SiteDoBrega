namespace WebApi.DTOs
{
    public class CategoriaDto
    {
        public int Id { get; set; }
        public string Desc { get; set; }
        public string Url { get; set; }
        public bool Ativo { get; set; }
        public string Img { get; set; }

        public List<FaixaDto>? Faixas { get; set; }
    }
    public class CreateCategoriaDto
    {
        public string Desc { get; set; }
        public string Url { get; set; }
        public bool Ativo { get; set; }
        public string Img { get; set; }

        public List<CreateFaixaDto>? Faixas { get; set; }
    }
}
