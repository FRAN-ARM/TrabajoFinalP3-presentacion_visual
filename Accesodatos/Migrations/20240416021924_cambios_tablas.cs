using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accesodatos.Migrations
{
    public partial class cambios_tablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagen",
                table: "Libros",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "stock",
                table: "Libros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "fecha_nacimiento",
                table: "Clientes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "foto_perfil",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagen",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "stock",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "fecha_nacimiento",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "foto_perfil",
                table: "AspNetUsers");
        }
    }
}
