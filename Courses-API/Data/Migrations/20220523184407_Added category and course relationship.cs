using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses_API.Data.Migrations
{
  public partial class Addedcategoryandcourserelationship : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Category",
          table: "Courses");

      migrationBuilder.AddColumn<int>(
          name: "CategoryId",
          table: "Courses",
          type: "INTEGER",
          nullable: false,
          defaultValue: 0);

      migrationBuilder.CreateTable(
          name: "Categories",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Name = table.Column<string>(type: "TEXT", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Categories", x => x.Id);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Courses_CategoryId",
          table: "Courses",
          column: "CategoryId");

      migrationBuilder.AddForeignKey(
          name: "FK_Courses_Categories_CategoryId",
          table: "Courses",
          column: "CategoryId",
          principalTable: "Categories",
          principalColumn: "Id",
          onDelete: ReferentialAction.SetNull);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Courses_Categories_CategoryId",
          table: "Courses");

      migrationBuilder.DropTable(
          name: "Categories");

      migrationBuilder.DropIndex(
          name: "IX_Courses_CategoryId",
          table: "Courses");

      migrationBuilder.DropColumn(
          name: "CategoryId",
          table: "Courses");

      migrationBuilder.AddColumn<string>(
          name: "Category",
          table: "Courses",
          type: "TEXT",
          nullable: true);
    }
  }
}
