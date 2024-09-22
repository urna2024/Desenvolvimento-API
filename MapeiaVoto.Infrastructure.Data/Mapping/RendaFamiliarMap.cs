using MapeiaVoto.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Infrastructure.Data.Mapping
{
    public class RendaFamiliarMap : IEntityTypeConfiguration<RendaFamiliar>
    {
        public void Configure(EntityTypeBuilder<RendaFamiliar> builder)
        {
            builder.ToTable("RendaFamiliar"); // Nome da tabela no banco de dados
            builder.HasKey(e => e.id); // Definição de chave primária


            builder.Property(e => e.nome)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(100)") // Tipo da coluna
                .HasColumnName("descricao"); // Nome da coluna no banco de dados

            builder.HasMany(s => s.entrevistado)
                .WithOne(r => r.rendafamiliar)
                .HasForeignKey(r => r.idRendaFamiliar)
                .OnDelete(DeleteBehavior.Restrict); // Definir o comportamento de deleção
        }
    }
}
