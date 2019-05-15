using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns : table => new
                {
                    UserID = table.Column<Guid>(nullable: false),
                        Username = table.Column<string>(nullable: true),
                        PasswordHash = table.Column<byte[]>(nullable: false),
                        PasswordSalt = table.Column<byte[]>(nullable: false)
                },
                constraints : table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}