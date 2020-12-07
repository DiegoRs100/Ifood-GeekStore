using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekStore.Data.Migrations
{
    public partial class CorrecaoTabelasProdutoImagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "TB_IMAGENS");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "TB_PRODUTOS",
                newName: "Nome");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "TB_IMAGENS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "TB_IMAGENS");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "TB_PRODUTOS",
                newName: "Descricao");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "TB_IMAGENS",
                type: "VARCHAR(100)",
                nullable: true);
        }
    }
}
