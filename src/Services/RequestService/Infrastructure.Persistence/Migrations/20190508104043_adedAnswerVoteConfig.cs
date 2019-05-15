using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Infrastructure.Persistence.Migrations
{
    public partial class adedAnswerVoteConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AnswerVotes",
                newName: "AnswerVoteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnswerVoteID",
                table: "AnswerVotes",
                newName: "Id");
        }
    }
}