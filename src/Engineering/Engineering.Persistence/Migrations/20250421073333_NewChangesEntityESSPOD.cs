using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewChangesEntityESSPOD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculatedAmount",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails");

            migrationBuilder.RenameColumn(
                name: "TotalPercentage",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                newName: "SupervisorWorkVolume");

            migrationBuilder.RenameColumn(
                name: "StatusStatementWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                newName: "EmployerRepresentativeWorkVolume");

            migrationBuilder.RenameColumn(
                name: "DoneWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                newName: "ContractorWorkVolume");

            migrationBuilder.RenameColumn(
                name: "DonePercentage",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                newName: "ConsultantWorkVolume");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                type: "nvarchar(1500)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails");

            migrationBuilder.RenameColumn(
                name: "SupervisorWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                newName: "TotalPercentage");

            migrationBuilder.RenameColumn(
                name: "EmployerRepresentativeWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                newName: "StatusStatementWorkVolume");

            migrationBuilder.RenameColumn(
                name: "ContractorWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                newName: "DoneWorkVolume");

            migrationBuilder.RenameColumn(
                name: "ConsultantWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                newName: "DonePercentage");

            migrationBuilder.AddColumn<decimal>(
                name: "CalculatedAmount",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperationDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
