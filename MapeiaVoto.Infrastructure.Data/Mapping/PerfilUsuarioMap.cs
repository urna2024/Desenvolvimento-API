﻿using MapeiaVoto.Domain.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Infrastructure.Data.Mapping
{
    public class PerfilUsuarioMap : IEntityTypeConfiguration<PerfilUsuario>
    {
        public void Configure(EntityTypeBuilder<PerfilUsuario> builder)
        {
            builder.ToTable("PerfilUsuario"); // Nome da tabela no banco
            builder.HasKey(e => e.id); // Definição de chave primária

            builder.Property(e => e.nome)
                .IsRequired() // Campo requerido
                .HasColumnType("varchar(20)") // Tipo da coluna
                .HasColumnName("nome"); // Nome da coluna no banco de dados

            // Definir a relação com Representante
            builder.HasMany(s => s.usuario)
                .WithOne(r => r.perfilusuario)
                .HasForeignKey(r => r.idPerfilUsuario)
                .OnDelete(DeleteBehavior.Restrict); // Definir o comportamento de deleção
        }
    }
}
