using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquilibraMais.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToFuncionarioInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DATA",
                table: "FUNCIONARIO_INFO",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_SETOR_ID",
                table: "USUARIO",
                column: "SETOR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SETOR_EMPRESA_ID",
                table: "SETOR",
                column: "EMPRESA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_INFO_USUARIO_ID",
                table: "FUNCIONARIO_INFO",
                column: "USUARIO_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_FUNCIONARIO_INFO_USUARIO_USUARIO_ID",
                table: "FUNCIONARIO_INFO",
                column: "USUARIO_ID",
                principalTable: "USUARIO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SETOR_EMPRESA",
                table: "SETOR",
                column: "EMPRESA_ID",
                principalTable: "EMPRESA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_SETOR",
                table: "USUARIO",
                column: "SETOR_ID",
                principalTable: "SETOR",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FUNCIONARIO_INFO_USUARIO_USUARIO_ID",
                table: "FUNCIONARIO_INFO");

            migrationBuilder.DropForeignKey(
                name: "FK_SETOR_EMPRESA",
                table: "SETOR");

            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_SETOR",
                table: "USUARIO");

            migrationBuilder.DropIndex(
                name: "IX_USUARIO_SETOR_ID",
                table: "USUARIO");

            migrationBuilder.DropIndex(
                name: "IX_SETOR_EMPRESA_ID",
                table: "SETOR");

            migrationBuilder.DropIndex(
                name: "IX_FUNCIONARIO_INFO_USUARIO_ID",
                table: "FUNCIONARIO_INFO");

            migrationBuilder.DropColumn(
                name: "DATA",
                table: "FUNCIONARIO_INFO");
        }
    }
}
