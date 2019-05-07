using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Infrastructure.Persistence.Migrations
{
    public partial class UpdatedRequest : Migration
    {
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool> (
                name: "IsClosed",
                table: "Requests",
                nullable : false,
                defaultValue : false);
        }

        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn (
                name: "IsClosed",
                table: "Requests");
        }
    }
}