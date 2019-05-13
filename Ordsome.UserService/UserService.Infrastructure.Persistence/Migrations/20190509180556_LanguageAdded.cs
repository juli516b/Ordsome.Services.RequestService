using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Infrastructure.Persistence.Migrations
{
    public partial class LanguageAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Language",
                columns : table => new
                {
                    LanguageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                        LanguageCode = table.Column<string>(nullable: true),
                        LanguageName = table.Column<string>(nullable: true),
                        LanguageNativeName = table.Column<string>(nullable: true),
                        UserId = table.Column<Guid>(nullable: true)
                },
                constraints : table =>
                {
                    table.PrimaryKey("PK_Language", x => x.LanguageID);
                    table.ForeignKey(
                        name: "FK_Language_Users_UserId",
                        column : x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete : ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Language_UserId",
                table: "Language",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Language");
        }
    }
}