using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Answer_Requests_RequestId",
                "Answer");

            migrationBuilder.DropPrimaryKey(
                "PK_Answer",
                "Answer");

            migrationBuilder.RenameTable(
                "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex(
                "IX_Answer_RequestId",
                table: "Answers",
                newName: "IX_Answers_RequestId");

            migrationBuilder.AddPrimaryKey(
                "PK_Answers",
                "Answers",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_Answers_Requests_RequestId",
                "Answers",
                "RequestId",
                "Requests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Answers_Requests_RequestId",
                "Answers");

            migrationBuilder.DropPrimaryKey(
                "PK_Answers",
                "Answers");

            migrationBuilder.RenameTable(
                "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                "IX_Answers_RequestId",
                table: "Answer",
                newName: "IX_Answer_RequestId");

            migrationBuilder.AddPrimaryKey(
                "PK_Answer",
                "Answer",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_Answer_Requests_RequestId",
                "Answer",
                "RequestId",
                "Requests",
                principalColumn: "RequestID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}