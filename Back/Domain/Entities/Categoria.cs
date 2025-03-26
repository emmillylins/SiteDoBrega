namespace Domain.Entities
{
    public class Categoria
    {
        public Categoria() { }

        public Categoria(string desc, string url, string img, bool? ativo = true)
        {
            Desc = desc;
            Url = url;
            Img = img;
            Ativo = ativo;
        }

        public int Id { get; set; }
        public string Desc { get; set; }
        public string Url { get; set; }
        public string Img { get; set; }
        public bool? Ativo { get; set; }

        public List<Faixa> Faixas { get; set; } = [];
    }
}
