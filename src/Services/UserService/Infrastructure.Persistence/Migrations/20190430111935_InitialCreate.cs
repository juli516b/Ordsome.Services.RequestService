using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    UserID = table.Column<Guid>(),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(),
                    PasswordSalt = table.Column<byte[]>()
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.UserID); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Users");
        }
    }
}