using System;
using System.Collections.Generic;
using Library.Common.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Library.Data.Migrations
{
    public partial class Library : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
     name: "Books",
     columns: table => new
     {
         Id = table.Column<Guid>(nullable: false),
         Title = table.Column<string>(nullable: false),
         Author = table.Column<string>(nullable: false),
         YearOfPublication = table.Column<int>(nullable: false),
         Language = table.Column<string>(nullable: false),
         Count = table.Column<int>(nullable: false),
         CountPages = table.Column<int>(nullable: false),
         Categories = table.Column<List<BookCategory>>(nullable: false),
         KeyWordsId = table.Column<List<Guid>>(nullable: false),
         Description = table.Column<string>(nullable: false),
         URL = table.Column<string>(nullable: true),
         Cover = table.Column<byte[]>(nullable: true)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_Books", x => x.Id);
     });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

        }
    }
}
