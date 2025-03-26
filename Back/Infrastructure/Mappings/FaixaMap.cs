using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class FaixaMap : IEntityTypeConfiguration<Faixa>
    {
        public void Configure(EntityTypeBuilder<Faixa> builder)
        {
            builder.ToTable("Faixas").HasKey(k => k.Id);

            builder.Property(p => p.Id).UseIdentityColumn();
            builder.Property(p => p.Titulo).HasColumnType("varchar(100)");
            builder.Property(p => p.Artista).HasColumnType("varchar(100)");
            builder.Property(p => p.Link).HasColumnType("varchar(500)");

            builder.HasIndex(i => i.UsuarioId).IsUnique(false);
            builder.HasIndex(i => i.CategoriaId).IsUnique(false);
        }
    }
}
