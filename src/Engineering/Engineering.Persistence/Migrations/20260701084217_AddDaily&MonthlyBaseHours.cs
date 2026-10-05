using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyMonthlyBaseHours : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DailyBaseHours",
                schema: "engineer",
                table: "ContractorContracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyBaseHours",
                schema: "engineer",
                table: "ContractorContracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DailyBaseHours",
                schema: "engineer",
                table: "ContractorContractHistories",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MonthlyBaseHours",
                schema: "engineer",
                table: "ContractorContractHistories",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyBaseHours",
                schema: "engineer",
                table: "ContractorContracts");

            migrationBuilder.DropColumn(
                name: "MonthlyBaseHours",
                schema: "engineer",
                table: "ContractorContracts");

            migrationBuilder.DropColumn(
                name: "DailyBaseHours",
                schema: "engineer",
                table: "ContractorContractHistories");

            migrationBuilder.DropColumn(
                name: "MonthlyBaseHours",
                schema: "engineer",
                table: "ContractorContractHistories");
        }
    }
}
