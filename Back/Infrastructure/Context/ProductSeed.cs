using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ProductSeed
    {
        public ModelBuilder Seed(ModelBuilder mdBuilder)
        {
            //mdBuilder = SeedMulta(mdBuilder);
            //mdBuilder = SeedUsers(mdBuilder);

            return mdBuilder;
        }

        public ModelBuilder SeedMulta(ModelBuilder mdBuilder)
        {
            mdBuilder.Entity<Multa>().HasData(
                new Multa("123456", DateTime.Now, "A001", "Excesso de velocidade", "ABC-1234"),
                new Multa("654321", DateTime.Now, "A002", "Estacionamento proibido", "XYZ-9876"),
                new Multa("789123", DateTime.Now, "A003", "Avanço de sinal vermelho", "DEF-5678")
            );
            return mdBuilder;
        }
    }
}
