using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntrevistado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idGenero",
                table: "Entrevistado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idNivelEscolaridade",
                table: "Entrevistado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idRendaFamiliar",
                table: "Entrevistado",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Entrevistado_idGenero",
                table: "Entrevistado",
                column: "idGenero");

            migrationBuilder.CreateIndex(
                name: "IX_Entrevistado_idNivelEscolaridade",
                table: "Entrevistado",
                column: "idNivelEscolaridade");

            migrationBuilder.CreateIndex(
                name: "IX_Entrevistado_idRendaFamiliar",
                table: "Entrevistado",
                column: "idRendaFamiliar");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrevistado_Genero_idGenero",
                table: "Entrevistado",
                column: "idGenero",
                principalTable: "Genero",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entrevistado_NivelEscolaridade_idNivelEscolaridade",
                table: "Entrevistado",
                column: "idNivelEscolaridade",
                principalTable: "NivelEscolaridade",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Entrevistado_RendaFamiliar_idRendaFamiliar",
                table: "Entrevistado",
                column: "idRendaFamiliar",
                principalTable: "RendaFamiliar",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrevistado_Genero_idGenero",
                table: "Entrevistado");

            migrationBuilder.DropForeignKey(
                name: "FK_Entrevistado_NivelEscolaridade_idNivelEscolaridade",
                table: "Entrevistado");

            migrationBuilder.DropForeignKey(
                name: "FK_Entrevistado_RendaFamiliar_idRendaFamiliar",
                table: "Entrevistado");

            migrationBuilder.DropIndex(
                name: "IX_Entrevistado_idGenero",
                table: "Entrevistado");

            migrationBuilder.DropIndex(
                name: "IX_Entrevistado_idNivelEscolaridade",
                table: "Entrevistado");

            migrationBuilder.DropIndex(
                name: "IX_Entrevistado_idRendaFamiliar",
                table: "Entrevistado");

            migrationBuilder.DropColumn(
                name: "idGenero",
                table: "Entrevistado");

            migrationBuilder.DropColumn(
                name: "idNivelEscolaridade",
                table: "Entrevistado");

            migrationBuilder.DropColumn(
                name: "idRendaFamiliar",
                table: "Entrevistado");
        }
    }
}
