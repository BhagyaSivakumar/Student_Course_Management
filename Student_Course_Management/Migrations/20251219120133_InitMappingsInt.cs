using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Course_Management.Migrations
{
    /// <inheritdoc />
    public partial class InitMappingsInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Mappings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SpResults",
                columns: table => new
                {
                    Success = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mappings_CourseId",
                table: "Mappings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Mappings_StudentId",
                table: "Mappings",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mappings_Courses_CourseId",
                table: "Mappings",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mappings_Students_StudentId",
                table: "Mappings",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mappings_Courses_CourseId",
                table: "Mappings");

            migrationBuilder.DropForeignKey(
                name: "FK_Mappings_Students_StudentId",
                table: "Mappings");

            migrationBuilder.DropTable(
                name: "SpResults");

            migrationBuilder.DropIndex(
                name: "IX_Mappings_CourseId",
                table: "Mappings");

            migrationBuilder.DropIndex(
                name: "IX_Mappings_StudentId",
                table: "Mappings");

            migrationBuilder.AlterColumn<string>(
                name: "CourseId",
                table: "Mappings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
