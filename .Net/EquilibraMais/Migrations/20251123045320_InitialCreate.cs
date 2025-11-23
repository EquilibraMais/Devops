using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquilibraMais.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPRESA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME_EMPRESA = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SETOR",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRICAO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMPRESA_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SETOR", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SETOR_EMPRESA",
                        column: x => x.EMPRESA_ID,
                        principalTable: "EMPRESA",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NOME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CARGO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SETOR_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USUARIO_SETOR",
                        column: x => x.SETOR_ID,
                        principalTable: "SETOR",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FUNCIONARIO_INFO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HUMOR = table.Column<int>(type: "int", nullable: false),
                    ENERGIA = table.Column<int>(type: "int", nullable: false),
                    CARGA = table.Column<int>(type: "int", nullable: false),
                    SONO = table.Column<int>(type: "int", nullable: false),
                    OBSERVACAO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HISTORICO_MEDICO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USUARIO_ID = table.Column<int>(type: "int", nullable: false),
                    DATA = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIO_INFO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIO_INFO_USUARIO_USUARIO_ID",
                        column: x => x.USUARIO_ID,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_INFO_USUARIO_ID",
                table: "FUNCIONARIO_INFO",
                column: "USUARIO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SETOR_EMPRESA_ID",
                table: "SETOR",
                column: "EMPRESA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_SETOR_ID",
                table: "USUARIO",
                column: "SETOR_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FUNCIONARIO_INFO");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "SETOR");

            migrationBuilder.DropTable(
                name: "EMPRESA");
        }
    }
}
