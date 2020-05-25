using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace W_CC.Infra.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Nome = table.Column<string>(maxLength: 120, nullable: false),
                    CPF = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContasCorrentes",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Agencia = table.Column<int>(maxLength: 4, nullable: false),
                    Conta = table.Column<int>(maxLength: 10, nullable: false),
                    Saldo = table.Column<decimal>(nullable: false),
                    UltimaMovimentacao = table.Column<DateTime>(nullable: false),
                    PessoaId = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContasCorrentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContasCorrentes_Pessoas_PessoaId",
                        column: x => x.PessoaId,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Operacoes",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    ContaCorrenteId = table.Column<byte[]>(nullable: false),
                    TipoOperacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operacoes_ContasCorrentes_ContaCorrenteId",
                        column: x => x.ContaCorrenteId,
                        principalTable: "ContasCorrentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContasCorrentes_PessoaId",
                table: "ContasCorrentes",
                column: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContasCorrentes_Agencia_Conta",
                table: "ContasCorrentes",
                columns: new[] { "Agencia", "Conta" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operacoes_ContaCorrenteId",
                table: "Operacoes",
                column: "ContaCorrenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_CPF",
                table: "Pessoas",
                column: "CPF",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operacoes");

            migrationBuilder.DropTable(
                name: "ContasCorrentes");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
