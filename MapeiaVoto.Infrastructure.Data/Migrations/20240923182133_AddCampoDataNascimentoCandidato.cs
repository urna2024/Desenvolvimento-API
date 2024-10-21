using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCampoDataNascimentoCandidato : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidato_CargoDisputado_idCargoDisputado",
                table: "Candidato");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidato_PartidoPolitico_idPartidoPolitico",
                table: "Candidato");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidato_Status_idStatus",
                table: "Candidato");

            migrationBuilder.AlterColumn<string>(
                name: "uf",
                table: "Candidato",
                type: "varchar(2)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "nomeUrna",
                table: "Candidato",
                type: "varchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "nomeCompleto",
                table: "Candidato",
                type: "varchar(150)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "municipio",
                table: "Candidato",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "foto",
                table: "Candidato",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataNascimento",
                table: "Candidato",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "fk_cargodisputado_candidato",
                table: "Candidato",
                column: "idCargoDisputado",
                principalTable: "CargoDisputado",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_partidopolitico_candidato",
                table: "Candidato",
                column: "idPartidoPolitico",
                principalTable: "PartidoPolitico",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_status_candidato",
                table: "Candidato",
                column: "idStatus",
                principalTable: "Status",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cargodisputado_candidato",
                table: "Candidato");

            migrationBuilder.DropForeignKey(
                name: "fk_partidopolitico_candidato",
                table: "Candidato");

            migrationBuilder.DropForeignKey(
                name: "fk_status_candidato",
                table: "Candidato");

            migrationBuilder.AlterColumn<string>(
                name: "uf",
                table: "Candidato",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(2)");

            migrationBuilder.AlterColumn<string>(
                name: "nomeUrna",
                table: "Candidato",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)");

            migrationBuilder.AlterColumn<string>(
                name: "nomeCompleto",
                table: "Candidato",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(150)");

            migrationBuilder.AlterColumn<string>(
                name: "municipio",
                table: "Candidato",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "foto",
                table: "Candidato",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "dataNascimento",
                table: "Candidato",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidato_CargoDisputado_idCargoDisputado",
                table: "Candidato",
                column: "idCargoDisputado",
                principalTable: "CargoDisputado",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidato_PartidoPolitico_idPartidoPolitico",
                table: "Candidato",
                column: "idPartidoPolitico",
                principalTable: "PartidoPolitico",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Candidato_Status_idStatus",
                table: "Candidato",
                column: "idStatus",
                principalTable: "Status",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
