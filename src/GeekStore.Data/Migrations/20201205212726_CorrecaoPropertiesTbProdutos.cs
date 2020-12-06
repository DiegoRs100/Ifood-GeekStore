using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeekStore.Data.Migrations
{
    public partial class CorrecaoPropertiesTbProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "TB_PRODUTOS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "IdImagem",
                table: "TB_PRODUTOS",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ImagemId",
                table: "TB_PRODUTOS",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Preco",
                table: "TB_PRODUTOS",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "TB_IMAGENS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_TB_PRODUTOS_ImagemId",
                table: "TB_PRODUTOS",
                column: "ImagemId");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PRODUTOS_TB_IMAGENS_ImagemId",
                table: "TB_PRODUTOS",
                column: "ImagemId",
                principalTable: "TB_IMAGENS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_PRODUTOS_TB_IMAGENS_ImagemId",
                table: "TB_PRODUTOS");

            migrationBuilder.DropIndex(
                name: "IX_TB_PRODUTOS_ImagemId",
                table: "TB_PRODUTOS");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "TB_PRODUTOS");

            migrationBuilder.DropColumn(
                name: "IdImagem",
                table: "TB_PRODUTOS");

            migrationBuilder.DropColumn(
                name: "ImagemId",
                table: "TB_PRODUTOS");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "TB_PRODUTOS");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "TB_IMAGENS");
        }
    }
}
