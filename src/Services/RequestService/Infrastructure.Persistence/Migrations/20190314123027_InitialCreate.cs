using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Requests",
                table => new
                {
                    RequestID = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    LanguageOrigin = table.Column<string>(nullable: true),
                    LanguageTarget = table.Column<string>(),
                    TextToTranslate = table.Column<string>()
                },
                constraints: table => table.PrimaryKey("PK_Requests", x => x.RequestID));

            migrationBuilder.CreateTable(
                "Answer",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn),
                    TextTranslated = table.Column<string>(nullable: true),
                    RequestId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        "FK_Answer_Requests_RequestId",
                        x => x.RequestId,
                        "Requests",
                        "RequestID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_Answer_RequestId",
                "Answer",
                "RequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Answer");

            migrationBuilder.DropTable(
                "Requests");
        }
    }
}