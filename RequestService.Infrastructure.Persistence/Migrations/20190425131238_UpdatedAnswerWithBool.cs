using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.WebApi.Migrations
{
    public partial class UpdatedAnswerWithBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrectAnswer",
                table: "Answers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrectAnswer",
                table: "Answers");
        }
    }
}
