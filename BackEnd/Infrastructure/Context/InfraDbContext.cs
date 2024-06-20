using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class InfraDbContext : DbContext
    {
        public InfraDbContext(DbContextOptions<InfraDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Categoria> Generico { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasNoKey();

            ProductSeed productSeed = new ProductSeed();
            modelBuilder = productSeed.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);

            //Caso Tenhamos esquecido de setar um tamanho para varchar, ele irá substituir os valores max por 100
            foreach (var property in modelBuilder.Model.GetEntityTypes()
               .SelectMany(p => p.GetProperties()
                   .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(p => p.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;


            //Indica que os mappings estarão nesse projeto
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfraDbContext).Assembly);

        }
    }
}
