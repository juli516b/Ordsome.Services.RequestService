using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RequestService.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable (
                name: "Requests",
                columns : table => new
                {
                    RequestID = table.Column<int> (nullable: false)
                        .Annotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                        LanguageOrigin = table.Column<string> (nullable: true),
                        LanguageTarget = table.Column<string> (nullable: false),
                        TextToTranslate = table.Column<string> (nullable: false)
                },
                constraints : table => table.PrimaryKey ("PK_Requests", x => x.RequestID));

            migrationBuilder.CreateTable (
                name: "Answer",
                columns : table => new
                {
                    Id = table.Column<int> (nullable: false)
                        .Annotation ("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                        TextTranslated = table.Column<string> (nullable: true),
                        RequestId = table.Column<int> (nullable: false)
                },
                constraints : table =>
                {
                    table.PrimaryKey ("PK_Answer", x => x.Id);
                    table.ForeignKey (
                        name: "FK_Answer_Requests_RequestId",
                        column : x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "RequestID",
                        onDelete : ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex (
                name: "IX_Answer_RequestId",
                table: "Answer",
                column: "RequestId");
        }

        protected override void Down (MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable (
                name: "Answer");

            migrationBuilder.DropTable (
                name: "Requests");
        }
    }
}