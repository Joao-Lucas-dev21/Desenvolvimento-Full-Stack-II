using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atividade.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaEstruturaComEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "Produtos",
                newName: "Descricao");

            migrationBuilder.AddColumn<int>(
                name: "Estoque",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantidade",
                table: "ItemPedidos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estoque",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Quantidade",
                table: "ItemPedidos");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "Produtos",
                newName: "descricao");
        }
    }
}
