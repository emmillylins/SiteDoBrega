using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class UsuarioMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers").HasKey(k => k.Id);

            builder.Property(p => p.Cpf).HasColumnType("varchar(14)");
            builder.Property(p => p.DataNasc).HasColumnType("varchar(8)");

            builder.HasIndex(i => i.Cpf).IsUnique();

            builder.HasMany(p => p.Faixas).WithOne(c => c.Usuario)
                .HasForeignKey(fk => fk.UsuarioId).IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
