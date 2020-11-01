using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaselinaWeb.API.Migrations
{
    public partial class SeAgregaSupplieyProvider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contacto",
                table: "Clients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observaciones",
                table: "Clients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Provider",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),                    
                    Nombre = table.Column<string>(nullable: true),
                    Nit = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Observaciones = table.Column<string>(nullable: true),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplie",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),                    
                    Nombre = table.Column<string>(nullable: true),
                    Categoria = table.Column<string>(nullable: true),
                    CantidadMinima = table.Column<int>(nullable: false),
                    CantidadMaxima = table.Column<int>(nullable: false),
                    TieneIva = table.Column<bool>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplie", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provider");

            migrationBuilder.DropTable(
                name: "Supplie");

            migrationBuilder.DropColumn(
                name: "Contacto",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Observaciones",
                table: "Clients");
        }
    }
}
