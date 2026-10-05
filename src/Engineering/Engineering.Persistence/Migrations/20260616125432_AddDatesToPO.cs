using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDatesToPO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ActualFinishDate",
                schema: "engineer",
                table: "ProjectOperations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualStartDate",
                schema: "engineer",
                table: "ProjectOperations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaselineDuration",
                schema: "engineer",
                table: "ProjectOperations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BaselineFinishDate",
                schema: "engineer",
                table: "ProjectOperations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BaselineStartDate",
                schema: "engineer",
                table: "ProjectOperations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlannedDuration",
                schema: "engineer",
                table: "ProjectOperations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedFinishDate",
                schema: "engineer",
                table: "ProjectOperations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedStartDate",
                schema: "engineer",
                table: "ProjectOperations",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualFinishDate",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropColumn(
                name: "ActualStartDate",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropColumn(
                name: "BaselineDuration",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropColumn(
                name: "BaselineFinishDate",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropColumn(
                name: "BaselineStartDate",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropColumn(
                name: "PlannedDuration",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropColumn(
                name: "PlannedFinishDate",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.DropColumn(
                name: "PlannedStartDate",
                schema: "engineer",
                table: "ProjectOperations");
        }
    }
}
