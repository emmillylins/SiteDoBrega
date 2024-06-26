﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings
{
    internal class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios").HasKey(k => k.NomeUsuario);

            builder.Property(p => p.Cpf).HasColumnType("varchar(14)");
            builder.Property(p => p.Nome).HasColumnType("varchar(100)");
            builder.Property(p => p.NomeUsuario).HasColumnType("varchar(30)");
            builder.Property(p => p.DataNasc).HasColumnType("varchar(8)");

            builder.HasIndex(i => i.Cpf).IsUnique();

            builder.HasMany(p => p.Faixas).WithOne(c => c.Usuario)
                .HasForeignKey(fk => fk.NomeUsuario).IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
