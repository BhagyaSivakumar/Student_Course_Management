using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Course_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseCodeToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CouseId",
                table: "Mappings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseCode",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CouseId",
                table: "Mappings");

            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "Courses");
        }
    }
}
