using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses_API.Data.Migrations
{
    public partial class addedteachercompetence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Teachers_TeacherId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_TeacherId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "TeacherCompetences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeacherId = table.Column<int>(type: "INTEGER", nullable: false),
                    CompetenceId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherCompetences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherCompetences_Categories_CompetenceId",
                        column: x => x.CompetenceId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherCompetences_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCompetences_CompetenceId",
                table: "TeacherCompetences",
                column: "CompetenceId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherCompetences_TeacherId",
                table: "TeacherCompetences",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherCompetences");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Categories",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TeacherId",
                table: "Categories",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Teachers_TeacherId",
                table: "Categories",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }
    }
}
