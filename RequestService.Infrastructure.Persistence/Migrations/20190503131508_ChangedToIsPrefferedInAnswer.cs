using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Infrastructure.Persistence.Migrations
{
    public partial class ChangedToIsPrefferedInAnswer : Migration
    {
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn (
                name: "IsCorrectAnswer",
                table: "Answers",
                newName: "IsPreferred");
        }

        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn (
                name: "IsPreferred",
                table: "Answers",
                newName: "IsCorrectAnswer");
        }
    }
}