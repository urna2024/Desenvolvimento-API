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
    [Migration("20240923181210_AddCamposAjustadosEmCandidato")]
    partial class AddCamposAjustadosEmCandidato
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.Candidato", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("dataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("foto")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("idCargoDisputado")
                        .HasColumnType("int");

                    b.Property<int>("idPartidoPolitico")
                        .HasColumnType("int");

                    b.Property<int>("idStatus")
                        .HasColumnType("int");

                    b.Property<string>("municipio")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("nomeCompleto")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("nomeUrna")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("uf")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("id");

                    b.HasIndex("idCargoDisputado");

                    b.HasIndex("idPartidoPolitico");

                    b.HasIndex("idStatus");

                    b.ToTable("Candidato", (string)null);
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.CargoDisputado", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("id");

                    b.ToTable("CargoDisputado", (string)null);
                });

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

                    b.Property<int>("idGenero")
                        .HasColumnType("int");

                    b.Property<int>("idNivelEscolaridade")
                        .HasColumnType("int");

                    b.Property<int?>("idPesquisaEleitoralMunicipal")
                        .HasColumnType("int");

                    b.Property<int>("idRendaFamiliar")
                        .HasColumnType("int");

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

                    b.HasIndex("idGenero");

                    b.HasIndex("idNivelEscolaridade");

                    b.HasIndex("idPesquisaEleitoralMunicipal");

                    b.HasIndex("idRendaFamiliar");

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

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.NivelEscolaridade", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("nome");

                    b.HasKey("id");

                    b.ToTable("NivelEscolaridade", (string)null);
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
                        .HasColumnType("varchar(12)")
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

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.PesquisaEleitoralMunicipal", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("dataEntrevista")
                        .HasColumnType("datetime")
                        .HasColumnName("dataEntrevista");

                    b.Property<int?>("idCandidatoPrefeito")
                        .HasColumnType("int");

                    b.Property<int?>("idCandidatoVereador")
                        .HasColumnType("int");

                    b.Property<int>("idStatus")
                        .HasColumnType("int");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int");

                    b.Property<string>("municipio")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("municipio");

                    b.Property<string>("sugestaoMelhoria")
                        .IsRequired()
                        .HasColumnType("varchar(500)")
                        .HasColumnName("sugestaoMelhoria");

                    b.Property<string>("uf")
                        .IsRequired()
                        .HasColumnType("varchar(2)")
                        .HasColumnName("uf");

                    b.Property<bool>("votoBrancoNulo")
                        .HasColumnType("bit")
                        .HasColumnName("votoBrancoNulo");

                    b.Property<bool>("votoIndeciso")
                        .HasColumnType("bit")
                        .HasColumnName("votoIndeciso");

                    b.HasKey("id");

                    b.HasIndex("idCandidatoPrefeito");

                    b.HasIndex("idCandidatoVereador");

                    b.HasIndex("idStatus");

                    b.HasIndex("idUsuario");

                    b.ToTable("PesquisaEleitoralMunicipal", (string)null);
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.RendaFamiliar", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("descricao");

                    b.HasKey("id");

                    b.ToTable("RendaFamiliar", (string)null);
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

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<int>("idPerfilUsuario")
                        .HasColumnType("int");

                    b.Property<int>("idStatus")
                        .HasColumnType("int");

                    b.Property<string>("nomeUsuario")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("nomeUsuario");

                    b.Property<bool>("precisaTrocarSenha")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true)
                        .HasColumnName("precisaTrocarSenha");

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("senha");

                    b.HasKey("id");

                    b.HasIndex("idPerfilUsuario");

                    b.HasIndex("idStatus");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.Candidato", b =>
                {
                    b.HasOne("MapeiaVoto.Domain.Entidades.CargoDisputado", "cargodisputado")
                        .WithMany("candidato")
                        .HasForeignKey("idCargoDisputado")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MapeiaVoto.Domain.Entidades.PartidoPolitico", "partidopolitico")
                        .WithMany("candidato")
                        .HasForeignKey("idPartidoPolitico")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MapeiaVoto.Domain.Entidades.Status", "status")
                        .WithMany("candidato")
                        .HasForeignKey("idStatus")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("cargodisputado");

                    b.Navigation("partidopolitico");

                    b.Navigation("status");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.Entrevistado", b =>
                {
                    b.HasOne("MapeiaVoto.Domain.Entidades.Genero", "genero")
                        .WithMany("entrevistado")
                        .HasForeignKey("idGenero")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MapeiaVoto.Domain.Entidades.NivelEscolaridade", "nivelescolaridade")
                        .WithMany("entrevistado")
                        .HasForeignKey("idNivelEscolaridade")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MapeiaVoto.Domain.Entidades.PesquisaEleitoralMunicipal", "pesquisaeleitoralmunicipal")
                        .WithMany("entrevistado")
                        .HasForeignKey("idPesquisaEleitoralMunicipal")
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("fk_pesquisaeleitoralmunicipal_entrevistado");

                    b.HasOne("MapeiaVoto.Domain.Entidades.RendaFamiliar", "rendafamiliar")
                        .WithMany("entrevistado")
                        .HasForeignKey("idRendaFamiliar")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("genero");

                    b.Navigation("nivelescolaridade");

                    b.Navigation("pesquisaeleitoralmunicipal");

                    b.Navigation("rendafamiliar");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.PesquisaEleitoralMunicipal", b =>
                {
                    b.HasOne("MapeiaVoto.Domain.Entidades.Candidato", "candidatoPrefeito")
                        .WithMany()
                        .HasForeignKey("idCandidatoPrefeito")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MapeiaVoto.Domain.Entidades.Candidato", "candidatoVereador")
                        .WithMany()
                        .HasForeignKey("idCandidatoVereador")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MapeiaVoto.Domain.Entidades.Status", "status")
                        .WithMany("pesquisaeleitoralmunicipal")
                        .HasForeignKey("idStatus")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MapeiaVoto.Domain.Entidades.Usuario", "usuario")
                        .WithMany("pesquisaeleitoralmunicipal")
                        .HasForeignKey("idUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("candidatoPrefeito");

                    b.Navigation("candidatoVereador");

                    b.Navigation("status");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.Usuario", b =>
                {
                    b.HasOne("MapeiaVoto.Domain.Entidades.PerfilUsuario", "perfilusuario")
                        .WithMany("usuario")
                        .HasForeignKey("idPerfilUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MapeiaVoto.Domain.Entidades.Status", "status")
                        .WithMany("usuario")
                        .HasForeignKey("idStatus")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("perfilusuario");

                    b.Navigation("status");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.CargoDisputado", b =>
                {
                    b.Navigation("candidato");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.Genero", b =>
                {
                    b.Navigation("entrevistado");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.NivelEscolaridade", b =>
                {
                    b.Navigation("entrevistado");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.PartidoPolitico", b =>
                {
                    b.Navigation("candidato");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.PerfilUsuario", b =>
                {
                    b.Navigation("usuario");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.PesquisaEleitoralMunicipal", b =>
                {
                    b.Navigation("entrevistado");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.RendaFamiliar", b =>
                {
                    b.Navigation("entrevistado");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.Status", b =>
                {
                    b.Navigation("candidato");

                    b.Navigation("pesquisaeleitoralmunicipal");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("MapeiaVoto.Domain.Entidades.Usuario", b =>
                {
                    b.Navigation("pesquisaeleitoralmunicipal");
                });
#pragma warning restore 612, 618
        }
    }
}
