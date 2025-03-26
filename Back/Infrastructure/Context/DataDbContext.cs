using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<ApplicationUserToken> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var productSeed = new ProductSeed();
            builder = productSeed.Seed(builder);

            // Configure Identity tables if necessary
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(x => x.Id);
            builder.Entity<IdentityRole>().ToTable("AspNetRole").HasKey(x => x.Id);

            builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaim").HasNoKey();
            builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogin").HasNoKey();
            builder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaim").HasNoKey();

            builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRole").HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Entity<ApplicationUserToken>().ToTable("AspNetUserToken").HasKey(t => new { t.UserId, t.CreationDate, t.Value });

            base.OnModelCreating(builder);

            // Caso Tenhamos esquecido de setar um tamanho para varchar, ele irá substituir os valores max por 100
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(p => p.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            builder.Entity<ApplicationUserToken>().Property(t => t.Value).HasColumnType("varchar(500)").IsRequired();

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(p => p.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            // Indica que os mappings estarão nesse projeto
            builder.ApplyConfigurationsFromAssembly(typeof(DataDbContext).Assembly);
        }
    }

}

