namespace Domain.Entities
{
    public class Generico
    {
        public Generico() { }

        public Generico(int id, string desc)
        {
            Id = id;
            Desc = desc;
        }

        public int Id { get; set; }
        public string Desc { get; set; }
    }
}
