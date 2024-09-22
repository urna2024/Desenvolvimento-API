using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPesquisaEleitoralMunicipalComEntrevistado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idPesquisaEleitoralMunicipal",
                table: "Entrevistado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entrevistado_idPesquisaEleitoralMunicipal",
                table: "Entrevistado",
                column: "idPesquisaEleitoralMunicipal");

            migrationBuilder.AddForeignKey(
                name: "fk_pesquisaeleitoralmunicipal_entrevistado",
                table: "Entrevistado",
                column: "idPesquisaEleitoralMunicipal",
                principalTable: "PesquisaEleitoralMunicipal",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pesquisaeleitoralmunicipal_entrevistado",
                table: "Entrevistado");

            migrationBuilder.DropIndex(
                name: "IX_Entrevistado_idPesquisaEleitoralMunicipal",
                table: "Entrevistado");

            migrationBuilder.DropColumn(
                name: "idPesquisaEleitoralMunicipal",
                table: "Entrevistado");
        }
    }
}
