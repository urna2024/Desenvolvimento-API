using MapeiaVoto.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Infrastructure.Data.Mapping
{
    public class GeneroMap : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.ToTable("Genero"); // Nome da tabela no banco
            builder.HasKey(e => e.id); // Definição de chave primária

            builder.Property(e => e.nome)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(20)") // Tipo da coluna
                .HasColumnName("nome"); // Nome da coluna no banco de dados


        }
    }
}
