using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewChangesEntityESSPO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPercentage",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "SupervisorWorkVolume");

            migrationBuilder.RenameColumn(
                name: "StatusStatementWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "EmployerRepresentativeWorkVolume");

            migrationBuilder.RenameColumn(
                name: "StandardDeviation",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "EmployerCommercialUnitPrice");

            migrationBuilder.RenameColumn(
                name: "DoneWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "EmployerCommercialTotalPrice");

            migrationBuilder.RenameColumn(
                name: "DonePercentage",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "ContractorWorkVolume");

            migrationBuilder.RenameColumn(
                name: "CalculatedAmount",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "ContractorUnitPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "ConsultantWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ContractorConfirmeTotalPrice",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ContractorTotalPrice",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployerCommercialConfirmeTotalPrice",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                type: "nvarchar(1500)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsultantWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations");

            migrationBuilder.DropColumn(
                name: "ContractorConfirmeTotalPrice",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations");

            migrationBuilder.DropColumn(
                name: "ContractorTotalPrice",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations");

            migrationBuilder.DropColumn(
                name: "EmployerCommercialConfirmeTotalPrice",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations");

            migrationBuilder.RenameColumn(
                name: "SupervisorWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "TotalPercentage");

            migrationBuilder.RenameColumn(
                name: "EmployerRepresentativeWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "StatusStatementWorkVolume");

            migrationBuilder.RenameColumn(
                name: "EmployerCommercialUnitPrice",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "StandardDeviation");

            migrationBuilder.RenameColumn(
                name: "EmployerCommercialTotalPrice",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "DoneWorkVolume");

            migrationBuilder.RenameColumn(
                name: "ContractorWorkVolume",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "DonePercentage");

            migrationBuilder.RenameColumn(
                name: "ContractorUnitPrice",
                schema: "engineer",
                table: "EmployerStatusStatementProjectOperations",
                newName: "CalculatedAmount");
        }
    }
}
