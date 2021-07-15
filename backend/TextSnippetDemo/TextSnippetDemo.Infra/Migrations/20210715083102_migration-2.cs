using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TextSnippetDemo.Infra.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Caches",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ExpiresAtTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    SlidingExpirationInSeconds = table.Column<long>(type: "bigint", nullable: true),
                    AbsoluteExpiration = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caches", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caches");
        }
    }
}
