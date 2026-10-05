using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewContractorStatusStatementAmounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "FinalManagerConfirmedAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PrimaryManagerConfirmedAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalManagerConfirmedAmount",
                schema: "engineer",
                table: "ContractorStatusStatements");

            migrationBuilder.DropColumn(
                name: "PrimaryManagerConfirmedAmount",
                schema: "engineer",
                table: "ContractorStatusStatements");
        }
    }
}
