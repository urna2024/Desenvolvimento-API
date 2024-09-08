﻿// <auto-generated />
using System;
using MapeiaVoto.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    [DbContext(typeof(SqlServerContext))]
    [Migration("20240902184400_AddPartidoPoliticoEntity")]
    partial class AddPartidoPoliticoEntity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.Entrevistado", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("celular")
                        .HasColumnType("varchar(11)")
                        .HasColumnName("celular");

                    b.Property<DateTime>("dataNascimento")
                        .HasColumnType("date")
                        .HasColumnName("dataNascimento");

                    b.Property<string>("municipio")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("municipio");

                    b.Property<string>("nomeCompleto")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nomeCompleto");

                    b.Property<string>("uf")
                        .IsRequired()
                        .HasColumnType("varchar(2)")
                        .HasColumnName("uf");

                    b.HasKey("id");

                    b.ToTable("Entrevistado", (string)null);
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.Genero", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("nome");

                    b.HasKey("id");

                    b.ToTable("Genero", (string)null);
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.PartidoPolitico", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nome");

                    b.Property<string>("sigla")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("sigla");

                    b.HasKey("id");

                    b.ToTable("PartidoPolitico", (string)null);
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.PerfilUsuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("nome");

                    b.HasKey("id");

                    b.ToTable("PerfilUsuario", (string)null);
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.Status", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("nome");

                    b.HasKey("id");

                    b.ToTable("Status", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
