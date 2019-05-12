using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Infrastructure.Persistence.Migrations
{
    public partial class AddedUserIdToRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Requests",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Requests");
        }
    }
}
