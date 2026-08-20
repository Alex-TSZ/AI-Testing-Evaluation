using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AiBenchmarkAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionDifficulty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstimatedDiffficulty",
                table: "Questions",
                newName: "EstimatedDifficulty");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionType",
                table: "Questions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EstimatedDifficulty",
                table: "Questions",
                newName: "EstimatedDiffficulty");

            migrationBuilder.AlterColumn<string>(
                name: "QuestionType",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
