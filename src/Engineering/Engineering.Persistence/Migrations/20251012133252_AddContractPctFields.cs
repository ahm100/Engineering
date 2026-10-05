using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractPctFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PercentageOfFixContractTotalAmount",
                schema: "engineer",
                table: "ContractorStatusStatementDetails",
                newName: "ProjectFixedContractPct");

            migrationBuilder.AddColumn<decimal>(
                name: "FixedContractPct",
                schema: "engineer",
                table: "ContractorStatusStatementDetails",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FixedContractPctDesc",
                schema: "engineer",
                table: "ContractorStatusStatementDetails",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ManagerFixedContractPct",
                schema: "engineer",
                table: "ContractorStatusStatementDetails",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerFixedContractPctDesc",
                schema: "engineer",
                table: "ContractorStatusStatementDetails",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectFixedContractPctDesc",
                schema: "engineer",
                table: "ContractorStatusStatementDetails",
                type: "nvarchar(1500)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedContractPct",
                schema: "engineer",
                table: "ContractorStatusStatementDetails");

            migrationBuilder.DropColumn(
                name: "FixedContractPctDesc",
                schema: "engineer",
                table: "ContractorStatusStatementDetails");

            migrationBuilder.DropColumn(
                name: "ManagerFixedContractPct",
                schema: "engineer",
                table: "ContractorStatusStatementDetails");

            migrationBuilder.DropColumn(
                name: "ManagerFixedContractPctDesc",
                schema: "engineer",
                table: "ContractorStatusStatementDetails");

            migrationBuilder.DropColumn(
                name: "ProjectFixedContractPctDesc",
                schema: "engineer",
                table: "ContractorStatusStatementDetails");

            migrationBuilder.RenameColumn(
                name: "ProjectFixedContractPct",
                schema: "engineer",
                table: "ContractorStatusStatementDetails",
                newName: "PercentageOfFixContractTotalAmount");
        }
    }
}
