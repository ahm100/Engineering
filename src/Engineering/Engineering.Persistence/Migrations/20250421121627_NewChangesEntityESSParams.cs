using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewChangesEntityESSParams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalDailyWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDetailWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalDailyWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDailyWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations");

            migrationBuilder.DropColumn(
                name: "TotalDetailWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations");

            migrationBuilder.DropColumn(
                name: "TotalDailyWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails");
        }
    }
}
