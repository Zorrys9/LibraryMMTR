using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Data.Migrations
{
    public partial class ChangeBooksTalbe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfReceipt",
                table: "StatusLogs",
                newName: "Date");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Cover",
                table: "Books",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Aviable",
                table: "Books",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aviable",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "StatusLogs",
                newName: "DateOfReceipt");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Cover",
                table: "Books",
                nullable: true,
                oldClrType: typeof(byte[]));
        }
    }
}
