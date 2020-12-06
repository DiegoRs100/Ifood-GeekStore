using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekStore.Data.Migrations
{
    public partial class CriacaoTabelaIMagens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_IMAGENS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdUsuarioInclusao = table.Column<Guid>(nullable: false),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    IdUsuarioAlteracao = table.Column<Guid>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Path = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Extensao = table.Column<string>(type: "VARCHAR(5)", nullable: true)
                },
                constraints: table => table.PrimaryKey("PK_TB_IMAGENS", x => x.Id));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_IMAGENS");
        }
    }
}
