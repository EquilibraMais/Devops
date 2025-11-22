using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquilibraMais.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPRESA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME_EMPRESA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FUNCIONARIO_INFO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    HUMOR = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ENERGIA = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CARGA = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    SONO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    OBSERVACAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    HISTORICO_MEDICO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    USUARIO_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIO_INFO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SETOR",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    EMPRESA_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SETOR", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CARGO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SETOR_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPRESA");

            migrationBuilder.DropTable(
                name: "FUNCIONARIO_INFO");

            migrationBuilder.DropTable(
                name: "SETOR");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
