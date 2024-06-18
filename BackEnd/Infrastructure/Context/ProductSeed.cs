using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ProductSeed
    {
        public ModelBuilder Seed(ModelBuilder mdBuilder)
        {
            mdBuilder = SeedGenerico(mdBuilder);

            return mdBuilder;
        }

        public ModelBuilder SeedGenerico(ModelBuilder mdBuilder)
        {
            mdBuilder.Entity<Generico>().HasData(
                new Generico(1, ""),
                new Generico(2, "")
                );
            return mdBuilder;
        }
    }
}
