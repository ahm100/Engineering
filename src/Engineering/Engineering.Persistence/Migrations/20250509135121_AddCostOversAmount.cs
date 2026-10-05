using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCostOversAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CostOversAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CostOversAmount",
                schema: "engineer",
                table: "ContractorStatusStatementHistories",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostOversAmount",
                schema: "engineer",
                table: "ContractorStatusStatements");

            migrationBuilder.DropColumn(
                name: "CostOversAmount",
                schema: "engineer",
                table: "ContractorStatusStatementHistories");
        }
    }
}
