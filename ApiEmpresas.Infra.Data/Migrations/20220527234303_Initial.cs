using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEmpresas.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPRESA",
                columns: table => new
                {
                    IDEMPRESA = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOMEFANTASIA = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RAZAOSOCIAL = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.IDEMPRESA);
                });

            migrationBuilder.CreateTable(
                name: "FUNCIONARIO",
                columns: table => new
                {
                    IDFUNCIONARIO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NOME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    MATRICULA = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DATAADMISSAO = table.Column<DateTime>(type: "date", nullable: false),
                    IDEMPRESA = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIO", x => x.IDFUNCIONARIO);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIO_EMPRESA_IDEMPRESA",
                        column: x => x.IDEMPRESA,
                        principalTable: "EMPRESA",
                        principalColumn: "IDEMPRESA");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMPRESA_CNPJ",
                table: "EMPRESA",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EMPRESA_RAZAOSOCIAL",
                table: "EMPRESA",
                column: "RAZAOSOCIAL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_CPF",
                table: "FUNCIONARIO",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_IDEMPRESA",
                table: "FUNCIONARIO",
                column: "IDEMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_MATRICULA",
                table: "FUNCIONARIO",
                column: "MATRICULA",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FUNCIONARIO");

            migrationBuilder.DropTable(
                name: "EMPRESA");
        }
    }
}
