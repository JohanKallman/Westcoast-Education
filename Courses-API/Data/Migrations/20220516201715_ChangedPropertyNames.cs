using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Courses_API.Data.Migrations
{
    public partial class ChangedPropertyNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Courses",
                newName: "DurationUnit");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "Courses",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Courses",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "DurationUnit",
                table: "Courses",
                newName: "Subject");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
