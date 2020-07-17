using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaselinaWeb.API.Migrations
{
    public partial class SeAgregaCampoImagen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Imagen",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Products");
        }
    }
}
