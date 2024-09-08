using MapeiaVoto.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapeiaVoto.Infrastructure.Data.Mapping
{
    public class PartidoPoliticoMap : IEntityTypeConfiguration<PartidoPolitico>
    {
        public void Configure(EntityTypeBuilder<PartidoPolitico> builder)
        {
            builder.ToTable("PartidoPolitico"); // Nome da tabela no banco de dados
            builder.HasKey(e => e.id); // Definição de chave primária

            builder.Property(e => e.nome)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(100)") // Tipo da coluna
                .HasColumnName("nome"); // Nome da coluna no banco de dados

            builder.Property(e => e.sigla)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(10)") // Tipo da coluna
                .HasColumnName("sigla"); // Nome da coluna no banco de dados

            builder.HasMany(s => s.candidato)
            .WithOne(r => r.partidopolitico)
            .HasForeignKey(r => r.idPartidoPolitico)
            .OnDelete(DeleteBehavior.Restrict); // Definir o comportamento de deleção


        }
    }
}
