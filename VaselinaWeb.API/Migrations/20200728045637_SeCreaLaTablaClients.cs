using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaselinaWeb.API.Migrations
{
    public partial class SeCreaLaTablaClients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Nit = table.Column<string>(maxLength: 150, nullable: false),
                    Nombre = table.Column<string>(maxLength: 150, nullable: false),
                    Apellidos = table.Column<string>(maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Ciudad = table.Column<string>(nullable: true),
                    Pais = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
