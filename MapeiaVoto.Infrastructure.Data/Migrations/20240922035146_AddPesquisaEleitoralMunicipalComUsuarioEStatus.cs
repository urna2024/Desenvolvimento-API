using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPesquisaEleitoralMunicipalComUsuarioEStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idStatus",
                table: "PesquisaEleitoralMunicipal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idUsuario",
                table: "PesquisaEleitoralMunicipal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PesquisaEleitoralMunicipal_idStatus",
                table: "PesquisaEleitoralMunicipal",
                column: "idStatus");

            migrationBuilder.CreateIndex(
                name: "IX_PesquisaEleitoralMunicipal_idUsuario",
                table: "PesquisaEleitoralMunicipal",
                column: "idUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_PesquisaEleitoralMunicipal_Status_idStatus",
                table: "PesquisaEleitoralMunicipal",
                column: "idStatus",
                principalTable: "Status",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PesquisaEleitoralMunicipal_Usuario_idUsuario",
                table: "PesquisaEleitoralMunicipal",
                column: "idUsuario",
                principalTable: "Usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PesquisaEleitoralMunicipal_Status_idStatus",
                table: "PesquisaEleitoralMunicipal");

            migrationBuilder.DropForeignKey(
                name: "FK_PesquisaEleitoralMunicipal_Usuario_idUsuario",
                table: "PesquisaEleitoralMunicipal");

            migrationBuilder.DropIndex(
                name: "IX_PesquisaEleitoralMunicipal_idStatus",
                table: "PesquisaEleitoralMunicipal");

            migrationBuilder.DropIndex(
                name: "IX_PesquisaEleitoralMunicipal_idUsuario",
                table: "PesquisaEleitoralMunicipal");

            migrationBuilder.DropColumn(
                name: "idStatus",
                table: "PesquisaEleitoralMunicipal");

            migrationBuilder.DropColumn(
                name: "idUsuario",
                table: "PesquisaEleitoralMunicipal");
        }
    }
}
