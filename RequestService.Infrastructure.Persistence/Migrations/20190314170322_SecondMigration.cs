using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.WebApi.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey (
                name: "FK_Answer_Requests_RequestId",
                table: "Answer");

            migrationBuilder.DropPrimaryKey (
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.RenameTable (
                name: "Answer",
                newName: "Answers");

            migrationBuilder.RenameIndex (
                name: "IX_Answer_RequestId",
                table: "Answers",
                newName: "IX_Answers_RequestId");

            migrationBuilder.AddPrimaryKey (
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.AddForeignKey (
                name: "FK_Answers_Requests_RequestId",
                table: "Answers",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestID",
                onDelete : ReferentialAction.Cascade);
        }

        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey (
                name: "FK_Answers_Requests_RequestId",
                table: "Answers");

            migrationBuilder.DropPrimaryKey (
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameTable (
                name: "Answers",
                newName: "Answer");

            migrationBuilder.RenameIndex (
                name: "IX_Answers_RequestId",
                table: "Answer",
                newName: "IX_Answer_RequestId");

            migrationBuilder.AddPrimaryKey (
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.AddForeignKey (
                name: "FK_Answer_Requests_RequestId",
                table: "Answer",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "RequestID",
                onDelete : ReferentialAction.Cascade);
        }
    }
}