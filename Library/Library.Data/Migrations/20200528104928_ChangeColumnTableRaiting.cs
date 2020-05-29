using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Data.Migrations
{
    public partial class ChangeColumnTableRaiting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaitingBooks_AspNetUsers_UserId",
                table: "RaitingBooks");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RaitingBooks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Score",
                table: "RaitingBooks",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_RaitingBooks_AspNetUsers_UserId",
                table: "RaitingBooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaitingBooks_AspNetUsers_UserId",
                table: "RaitingBooks");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RaitingBooks",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "RaitingBooks",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddForeignKey(
                name: "FK_RaitingBooks_AspNetUsers_UserId",
                table: "RaitingBooks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
