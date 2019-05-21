using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class AddedAnswerVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                "Id",
                "Answers",
                "AnswerID");

            migrationBuilder.AlterColumn<string>(
                "TextTranslated",
                "Answers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                "UserId",
                "Answers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                "AnswerVotes",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    AnswerId = table.Column<int>(),
                    UserId = table.Column<Guid>(),
                    Like = table.Column<bool>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerVotes", x => x.Id);
                    table.ForeignKey(
                        "FK_AnswerVotes_Answers_AnswerId",
                        x => x.AnswerId,
                        "Answers",
                        "AnswerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_AnswerVotes_AnswerId",
                "AnswerVotes",
                "AnswerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "AnswerVotes");

            migrationBuilder.DropColumn(
                "UserId",
                "Answers");

            migrationBuilder.RenameColumn(
                "AnswerID",
                "Answers",
                "Id");

            migrationBuilder.AlterColumn<string>(
                "TextTranslated",
                "Answers",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}