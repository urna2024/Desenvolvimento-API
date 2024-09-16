using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddGeneroEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Verifica se a tabela "Genero" já existe antes de tentar criá-la
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Genero' and xtype='U')
                BEGIN
                    CREATE TABLE [Genero] (
                        [id] int NOT NULL IDENTITY,
                        [nome] varchar(20) NOT NULL,
                        CONSTRAINT [PK_Genero] PRIMARY KEY ([id])
                    )
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Verifica se a tabela "Genero" existe antes de tentar excluí-la
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sysobjects WHERE name='Genero' and xtype='U')
                BEGIN
                    DROP TABLE [Genero]
                END
            ");
        }
    }
}
