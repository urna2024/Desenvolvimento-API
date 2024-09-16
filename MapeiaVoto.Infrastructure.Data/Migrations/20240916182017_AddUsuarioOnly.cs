using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MapeiaVoto.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nomeUsuario = table.Column<string>(type: "varchar(255)", nullable: false),
                    senha = table.Column<string>(type: "varchar(255)", nullable: false),
                    idStatus = table.Column<int>(type: "int", nullable: false),
                    idPerfilUsuario = table.Column<int>(type: "int", nullable: false),
                    precisaTrocarSenha = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                    table.ForeignKey(
                        name: "FK_Usuario_PerfilUsuario_idPerfilUsuario",
                        column: x => x.idPerfilUsuario,
                        principalTable: "PerfilUsuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_Status_idStatus",
                        column: x => x.idStatus,
                        principalTable: "Status",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_idPerfilUsuario",
                table: "Usuario",
                column: "idPerfilUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_idStatus",
                table: "Usuario",
                column: "idStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
