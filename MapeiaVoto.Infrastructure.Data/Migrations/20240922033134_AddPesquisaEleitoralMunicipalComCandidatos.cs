using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPesquisaEleitoralMunicipalComCandidatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Usuario",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "idCandidatoPrefeito",
                table: "PesquisaEleitoralMunicipal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idCandidatoVereador",
                table: "PesquisaEleitoralMunicipal",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PesquisaEleitoralMunicipal_idCandidatoPrefeito",
                table: "PesquisaEleitoralMunicipal",
                column: "idCandidatoPrefeito");

            migrationBuilder.CreateIndex(
                name: "IX_PesquisaEleitoralMunicipal_idCandidatoVereador",
                table: "PesquisaEleitoralMunicipal",
                column: "idCandidatoVereador");

            migrationBuilder.AddForeignKey(
                name: "FK_PesquisaEleitoralMunicipal_Candidato_idCandidatoPrefeito",
                table: "PesquisaEleitoralMunicipal",
                column: "idCandidatoPrefeito",
                principalTable: "Candidato",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PesquisaEleitoralMunicipal_Candidato_idCandidatoVereador",
                table: "PesquisaEleitoralMunicipal",
                column: "idCandidatoVereador",
                principalTable: "Candidato",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PesquisaEleitoralMunicipal_Candidato_idCandidatoPrefeito",
                table: "PesquisaEleitoralMunicipal");

            migrationBuilder.DropForeignKey(
                name: "FK_PesquisaEleitoralMunicipal_Candidato_idCandidatoVereador",
                table: "PesquisaEleitoralMunicipal");

            migrationBuilder.DropIndex(
                name: "IX_PesquisaEleitoralMunicipal_idCandidatoPrefeito",
                table: "PesquisaEleitoralMunicipal");

            migrationBuilder.DropIndex(
                name: "IX_PesquisaEleitoralMunicipal_idCandidatoVereador",
                table: "PesquisaEleitoralMunicipal");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "idCandidatoPrefeito",
                table: "PesquisaEleitoralMunicipal");

            migrationBuilder.DropColumn(
                name: "idCandidatoVereador",
                table: "PesquisaEleitoralMunicipal");
        }
    }
}
