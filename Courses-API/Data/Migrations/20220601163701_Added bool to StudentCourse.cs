using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses_API.Data.Migrations
{
    public partial class AddedbooltoStudentCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "StudentCourses",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "StudentCourses");
        }
    }
}
