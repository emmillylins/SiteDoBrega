using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias").HasKey(k => k.Id);

            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Desc).HasColumnType("varchar(30)");
            builder.Property(p => p.Url).HasColumnType("varchar(30)");
            builder.Property(p => p.Img).HasColumnType("varchar(200)");

            builder.HasMany(p => p.Faixas).WithOne(c => c.Categoria)
                .HasForeignKey(fk => fk.CategoriaId).IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
