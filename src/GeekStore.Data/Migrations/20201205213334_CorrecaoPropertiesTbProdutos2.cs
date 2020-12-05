using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Data.Migrations
{
    public partial class CorrecaoPropertiesTbProdutos2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_PRODUTOS_TB_IMAGENS_ImagemId",
                table: "TB_PRODUTOS");

            migrationBuilder.DropIndex(
                name: "IX_TB_PRODUTOS_ImagemId",
                table: "TB_PRODUTOS");

            migrationBuilder.DropColumn(
                name: "ImagemId",
                table: "TB_PRODUTOS");

            migrationBuilder.CreateIndex(
                name: "IX_TB_PRODUTOS_IdImagem",
                table: "TB_PRODUTOS",
                column: "IdImagem",
                unique: true,
                filter: "[IdImagem] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PRODUTOS_TB_IMAGENS_IdImagem",
                table: "TB_PRODUTOS",
                column: "IdImagem",
                principalTable: "TB_IMAGENS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_PRODUTOS_TB_IMAGENS_IdImagem",
                table: "TB_PRODUTOS");

            migrationBuilder.DropIndex(
                name: "IX_TB_PRODUTOS_IdImagem",
                table: "TB_PRODUTOS");

            migrationBuilder.AddColumn<Guid>(
                name: "ImagemId",
                table: "TB_PRODUTOS",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
