using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class MultaMap : IEntityTypeConfiguration<Multa>
    {
        public void Configure(EntityTypeBuilder<Multa> builder)
        {
            builder.ToTable("Multas").HasKey(k => k.Id);

            builder.Property(p => p.NumeroAIT).HasColumnType("varchar(50)");
            builder.Property(p => p.DataInfracao).HasColumnType("datetime2");
            builder.Property(p => p.CodigoInfracao).HasColumnType("varchar(50)");
            builder.Property(p => p.DescricaoInfracao).HasColumnType("varchar(50)");
            builder.Property(p => p.PlacaVeiculo).HasColumnType("varchar(10)");
            builder.Property(p => p.DataCriacao).HasColumnType("datetime2").HasDefaultValue(DateTime.Now);

            builder.Ignore(p => p.PermiteEdicao);
        }
    }
}
