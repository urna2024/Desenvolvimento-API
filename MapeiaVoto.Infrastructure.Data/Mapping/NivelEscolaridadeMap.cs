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
    public class NivelEscolaridadeMap : IEntityTypeConfiguration<NivelEscolaridade>
    {
        public void Configure(EntityTypeBuilder<NivelEscolaridade> builder)
        {
            builder.ToTable("NivelEscolaridade"); // Nome da tabela no banco de dados
            builder.HasKey(e => e.id); // Definição de chave primária

            builder.Property(e => e.nome)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(100)") // Tipo da coluna
                .HasColumnName("nome"); // Nome da coluna no banco de dados

            builder.HasMany(s => s.entrevistado)
                .WithOne(r => r.nivelescolaridade)
                .HasForeignKey(r => r.idNivelEscolaridade)
                .OnDelete(DeleteBehavior.Restrict); // Definir o comportamento de deleção


        }
    }
}
