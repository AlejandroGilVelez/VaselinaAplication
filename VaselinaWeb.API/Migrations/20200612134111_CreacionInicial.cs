using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaselinaWeb.API.Migrations
{
    public partial class CreacionInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    Nombres = table.Column<string>(maxLength: 150, nullable: false),
                    Empresa = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(maxLength: 100, nullable: false),
                    Mensaje = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FechaCreacion = table.Column<DateTime>(nullable: false),
                    FechaModificacion = table.Column<DateTime>(nullable: false),
                    NroIdentificacion = table.Column<long>(nullable: false),
                    Nombres = table.Column<string>(maxLength: 150, nullable: false),
                    Apellidos = table.Column<string>(maxLength: 150, nullable: false),
                    Correo = table.Column<string>(maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(maxLength: 100, nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
