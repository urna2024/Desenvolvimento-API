using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    public partial class AddPartidoPoliticoEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Verifica se a tabela PartidoPolitico já existe antes de tentar criá-la
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PartidoPolitico' and xtype='U')
                BEGIN
                    CREATE TABLE [PartidoPolitico] (
                        [id] int NOT NULL IDENTITY,
                        [nome] varchar(100) NOT NULL,
                        [sigla] varchar(10) NOT NULL,
                        CONSTRAINT [PK_PartidoPolitico] PRIMARY KEY ([id])
                    )
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Verifica se a tabela PartidoPolitico existe antes de tentar excluí-la
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT * FROM sysobjects WHERE name='PartidoPolitico' and xtype='U')
                BEGIN
                    DROP TABLE [PartidoPolitico]
                END
            ");
        }
    }
}
