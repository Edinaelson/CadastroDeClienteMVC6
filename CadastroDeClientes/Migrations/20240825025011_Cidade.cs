﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadastroDeClientes.Migrations
{
    public partial class Cidade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Cliente",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Cliente");
        }
    }
}
