using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MoveIncreaseRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncreaseRate",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.DropColumn(
                name: "IncreaseRate",
                schema: "engineer",
                table: "EmployerOperationProducts");

            migrationBuilder.AddColumn<decimal>(
                name: "IncreaseRate",
                schema: "engineer",
                table: "EmployerOperations",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 1m,
                comment: "ضریب افزایش");

            migrationBuilder.AddColumn<decimal>(
                name: "IncreaseRate",
                schema: "engineer",
                table: "EmployerOperationHistories",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 1m,
                comment: "ضریب افزایش");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IncreaseRate",
                schema: "engineer",
                table: "EmployerOperations");

            migrationBuilder.DropColumn(
                name: "IncreaseRate",
                schema: "engineer",
                table: "EmployerOperationHistories");

            migrationBuilder.AddColumn<decimal>(
                name: "IncreaseRate",
                schema: "engineer",
                table: "EmployerOperationServices",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "ضریب افزایش");

            migrationBuilder.AddColumn<decimal>(
                name: "IncreaseRate",
                schema: "engineer",
                table: "EmployerOperationProducts",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "ضریب افزایش");
        }
    }
}
