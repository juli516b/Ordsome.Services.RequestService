using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class ChangedToIsPrefferedInAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "IsCorrectAnswer",
                "Answers",
                "IsPreferred");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "IsPreferred",
                "Answers",
                "IsCorrectAnswer");
        }
    }
}