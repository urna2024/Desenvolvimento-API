using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    public partial class AddPerfilUsuarioEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Verifica se a tabela PerfilUsuario já existe antes de tentar criá-la
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PerfilUsuario' and xtype='U')
                BEGIN
                    CREATE TABLE [PerfilUsuario] (
                        [id] int NOT NULL IDENTITY,
                        [nome] varchar(20) NOT NULL,
                        CONSTRAINT [PK_PerfilUsuario] PRIMARY KEY ([id])
                    )
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Verifica se a tabela PerfilUsuario existe antes de tentar excluí-la
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sysobjects WHERE name='PerfilUsuario' and xtype='U')
                BEGIN
                    DROP TABLE [PerfilUsuario]
                END
            ");
        }
    }
}
