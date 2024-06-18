using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    public class GenericoMap : IEntityTypeConfiguration<Generico>
    {
        public void Configure(EntityTypeBuilder<Generico> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Desc).HasColumnType("varchar(100)");
        }
    }
}
