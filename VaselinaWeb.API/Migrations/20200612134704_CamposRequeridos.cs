using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VaselinaWeb.API.Migrations
{
    public partial class CamposRequeridos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Mensaje",
                table: "Contacts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AlterColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]));

            migrationBuilder.AlterColumn<string>(
                name: "Mensaje",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
