using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Mappings
{
    public class FrutaMapping : IEntityTypeConfiguration<Fruta>
    {
        public void Configure(EntityTypeBuilder<Fruta> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(f => f.Descricao)
              .IsRequired()
              .HasColumnType("varchar(300)");

            builder.Property(f => f.Imagem)
              .IsRequired()
              .HasColumnType("varchar(500)");

            builder.Property(f => f.Valor)
              .IsRequired()
              .HasColumnType("decimal(10,2)");

            builder.Property(f => f.Quantidade)
              .IsRequired()
              .HasColumnType("int");

            builder.ToTable("TB_Frutas");
        }
    }
}
