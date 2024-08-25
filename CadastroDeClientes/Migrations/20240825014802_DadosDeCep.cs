using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroDeClientes.Migrations
{
    public partial class DadosDeCep : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Cliente",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Ibge",
                table: "Cliente",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Rua",
                table: "Cliente",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Uf",
                table: "Cliente",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Ibge",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Rua",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Uf",
                table: "Cliente");
        }
    }
}
