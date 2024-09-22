using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPesquisaEleitoralMunicipal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PesquisaEleitoralMunicipal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dataEntrevista = table.Column<DateTime>(type: "datetime", nullable: false),
                    uf = table.Column<string>(type: "varchar(2)", nullable: false),
                    municipio = table.Column<string>(type: "varchar(100)", nullable: false),
                    votoIndeciso = table.Column<bool>(type: "bit", nullable: false),
                    votoBrancoNulo = table.Column<bool>(type: "bit", nullable: false),
                    sugestaoMelhoria = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PesquisaEleitoralMunicipal", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PesquisaEleitoralMunicipal");
        }
    }
}
