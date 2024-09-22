using MapeiaVoto.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MapeiaVoto.Infrastructure.Data.Mapping
{
    public class PesquisaEleitoralMunicipalMap : IEntityTypeConfiguration<PesquisaEleitoralMunicipal>
    {
        public void Configure(EntityTypeBuilder<PesquisaEleitoralMunicipal> builder)
        {
            // Nome da tabela
            builder.ToTable("PesquisaEleitoralMunicipal");

            // Chave primária
            builder.HasKey(e => e.id);

            // Propriedade dataEntrevista
            builder.Property(e => e.dataEntrevista)
                .IsRequired()
                .HasColumnName("dataEntrevista")
                .HasColumnType("datetime");

            // Propriedade uf
            builder.Property(e => e.uf)
                .IsRequired()
                .HasColumnName("uf")
                .HasColumnType("varchar(2)");

            // Propriedade municipio
            builder.Property(e => e.municipio)
                .IsRequired()
                .HasColumnName("municipio")
                .HasColumnType("varchar(100)");

            // Propriedade votoIndeciso
            builder.Property(e => e.votoIndeciso)
                .IsRequired()
                .HasColumnName("votoIndeciso")
                .HasColumnType("bit");

            // Propriedade votoBrancoNulo
            builder.Property(e => e.votoBrancoNulo)
                .IsRequired()
                .HasColumnName("votoBrancoNulo")
                .HasColumnType("bit");

            // Propriedade sugestaoMelhoria
            builder.Property(e => e.sugestaoMelhoria)
                .HasColumnName("sugestaoMelhoria")
                .HasColumnType("varchar(500)");

            // Relacionamento com candidato a prefeito
            builder.HasOne(e => e.candidatoPrefeito)
                .WithMany() // Muitos candidatos podem ser selecionados em várias pesquisas
                .HasForeignKey(e => e.idCandidatoPrefeito)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento com candidato a vereador
            builder.HasOne(e => e.candidatoVereador)
                .WithMany()
                .HasForeignKey(e => e.idCandidatoVereador)
                .OnDelete(DeleteBehavior.Restrict);

            // Definir a relação com Status
            builder.HasOne(r => r.status)
                .WithMany(s => s.pesquisaeleitoralmunicipal)
                .HasForeignKey(r => r.idStatus)
                .OnDelete(DeleteBehavior.Restrict);

            // Definir a relação com Status
            builder.HasOne(r => r.usuario)
                .WithMany(s => s.pesquisaeleitoralmunicipal)
                .HasForeignKey(r => r.idUsuario)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
