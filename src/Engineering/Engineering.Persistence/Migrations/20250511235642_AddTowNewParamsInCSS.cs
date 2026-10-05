using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTowNewParamsInCSS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CanPayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPrice",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanPayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatements");

            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                schema: "engineer",
                table: "ContractorStatusStatements");

            migrationBuilder.DropColumn(
                name: "PayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatements");
        }
    }
}
