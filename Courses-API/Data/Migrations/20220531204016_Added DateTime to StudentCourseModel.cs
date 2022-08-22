using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses_API.Data.Migrations
{
    public partial class AddedDateTimetoStudentCourseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "StudentCourses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "StudentCourses",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "StudentCourses");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "StudentCourses");
        }
    }
}
