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

        public ModelBuilder SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>().HasData(               
                new ApplicationUser(TipoUsuario.Administrador)
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEH+UkC5Q1uQpWm3mNoPAK2B9MyzZUw23cI9srFpb+9Zjum9npUdytElC955FIF2xTg==",
                    SecurityStamp = "3TMGOJAFEIHQ63XUIV3NA2N2Z63E7RFH",
                    ConcurrencyStamp = "2bdd8b41-84db-4ba6-909b-b6d3aab08521",
                    PhoneNumber = "123456789",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = true,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            );
            return modelBuilder;
        }
    }
}
