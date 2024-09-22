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
    public class StatusMap : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status"); // Nome da tabela no banco
            builder.HasKey(e => e.id); // Definição de chave primária

            builder.Property(e => e.nome)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(20)") // Tipo da coluna
                .HasColumnName("nome"); // Nome da coluna no banco de dados

            builder.HasMany(s => s.candidato)
                .WithOne(r => r.status)
                .HasForeignKey(r => r.idStatus)
                .OnDelete(DeleteBehavior.Restrict); // Definir o comportamento de deleção

            // Definir a relação com Representante
            builder.HasMany(s => s.usuario)
                .WithOne(r => r.status)
                .HasForeignKey(r => r.idStatus)
                .OnDelete(DeleteBehavior.Restrict); // Definir o comportamento de deleção

            // Definir a relação com Representante
            builder.HasMany(s => s.pesquisaeleitoralmunicipal)
                .WithOne(r => r.status)
                .HasForeignKey(r => r.idStatus)
                .OnDelete(DeleteBehavior.Restrict); // Definir o comportamento de deleção

        }
    }
}
