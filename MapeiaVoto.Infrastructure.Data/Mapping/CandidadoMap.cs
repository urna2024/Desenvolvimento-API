using MapeiaVoto.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MapeiaVoto.Infrastructure.Data.Mapping
{
    public class CandidatoMap : IEntityTypeConfiguration<Candidato>
    {
        public void Configure(EntityTypeBuilder<Candidato> builder)
        {
            // Nome da tabela
            builder.ToTable("Candidato");

            // Definição da chave primária
            builder.HasKey(e => e.id);

            // Propriedade: nomeCompleto
            builder.Property(e => e.nomeCompleto)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(150)") // Tipo da coluna
                .HasColumnName("nomeCompleto"); // Nome da coluna no banco de dados

            // Propriedade: nomeUrna
            builder.Property(e => e.nomeUrna)
               .IsRequired() // Campo requerido
               .HasColumnType("varchar(150)") // Tipo da coluna
               .HasColumnName("nomeUrna"); // Nome da coluna no banco de dados

            // Propriedade: dataNascimento
            builder.Property(e => e.dataNascimento)
                .IsRequired() // Campo requerido
                .HasColumnType("datetime") // Tipo da coluna, mantendo 'datetime' para armazenar data e hora
                .HasColumnName("dataNascimento"); // Nome da coluna no banco de dados

            // Propriedade: uf
            builder.Property(e => e.uf)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(2)") // Tipo da coluna
                .HasColumnName("uf"); // Nome da coluna no banco de dados

            // Propriedade: municipio
            builder.Property(e => e.municipio)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(100)") // Tipo da coluna
                .HasColumnName("municipio"); // Nome da coluna no banco de dados

            // Propriedade: foto (opcional)
            builder.Property(e => e.foto)
                .HasColumnType("varchar(255)") // Tipo da coluna
                .HasColumnName("foto"); // Nome da coluna no banco de dados

            // Relacionamento com Status
            builder.HasOne(r => r.status)
                .WithMany(s => s.candidato)
                .HasForeignKey(r => r.idStatus)
                .OnDelete(DeleteBehavior.Restrict) // Comportamento de deleção
                .HasConstraintName("fk_status_candidato");

            // Relacionamento com CargoDisputado
            builder.HasOne(r => r.cargodisputado)
                .WithMany(s => s.candidato)
                .HasForeignKey(r => r.idCargoDisputado)
                .OnDelete(DeleteBehavior.Restrict) // Comportamento de deleção
                .HasConstraintName("fk_cargodisputado_candidato");

            // Relacionamento com PartidoPolitico
            builder.HasOne(r => r.partidopolitico)
                .WithMany(s => s.candidato)
                .HasForeignKey(r => r.idPartidoPolitico)
                .OnDelete(DeleteBehavior.Restrict) // Comportamento de deleção
                .HasConstraintName("fk_partidopolitico_candidato");
        }
    }
}
