using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCampoSiglaPartidoAumentada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "sigla",
                table: "PartidoPolitico",
                type: "varchar(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "sigla",
                table: "PartidoPolitico",
                type: "varchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(12)");
        }
    }
}
