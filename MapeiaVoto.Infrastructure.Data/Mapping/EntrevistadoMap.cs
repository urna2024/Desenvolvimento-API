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
    public class EntrevistadoMap : IEntityTypeConfiguration<Entrevistado>
    {
        public void Configure(EntityTypeBuilder<Entrevistado> builder)
        {
            builder.ToTable("Entrevistado"); // Nome da tabela no banco
            builder.HasKey(e => e.id); // Definição de chave primária

            builder.Property(e => e.nomeCompleto)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(100)") // Tipo da coluna
                .HasColumnName("nomeCompleto"); // Nome da coluna no banco de dados

            builder.Property(e => e.dataNascimento)
                .IsRequired() // Campo requerido
                .HasColumnType("date") // Tipo da coluna
                .HasColumnName("dataNascimento"); // Nome da coluna no banco de dados

            builder.Property(e => e.uf)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(2)") // Tipo da coluna
                .HasColumnName("uf"); // Nome da coluna no banco de dados

            builder.Property(e => e.municipio)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(100)") // Tipo da coluna
                .HasColumnName("municipio"); // Nome da coluna no banco de dados

            builder.Property(e => e.celular)
                .HasColumnType("varchar(11)") // Tipo da coluna
                .HasColumnName("celular"); // Nome da coluna no banco de dados


            // Definir a relação com Status
            builder.HasOne(r => r.genero)
                .WithMany(s => s.entrevistado)
                .HasForeignKey(r => r.idGenero)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.nivelescolaridade)
                .WithMany(s => s.entrevistado)
                .HasForeignKey(r => r.idNivelEscolaridade)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.rendafamiliar)
                .WithMany(s => s.entrevistado)
                .HasForeignKey(r => r.idRendaFamiliar)
                .OnDelete(DeleteBehavior.Restrict);

            //Relação com a tabela de Paciente 
            builder.HasOne(p => p.pesquisaeleitoralmunicipal).WithMany(c => c.entrevistado)
                .HasConstraintName("fk_pesquisaeleitoralmunicipal_entrevistado")
                .HasForeignKey(p => p.idPesquisaEleitoralMunicipal)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
