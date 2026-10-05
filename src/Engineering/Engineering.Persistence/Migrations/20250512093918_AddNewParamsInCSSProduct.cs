using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewParamsInCSSProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerInvoiceNumber",
                schema: "engineer",
                table: "ContractorStatusStatementProducts",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountOnInvoiceNumber",
                schema: "engineer",
                table: "ContractorStatusStatementProducts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OtherPrice",
                schema: "engineer",
                table: "ContractorStatusStatementProducts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "engineer",
                table: "ContractorStatusStatementProducts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TransferPrice",
                schema: "engineer",
                table: "ContractorStatusStatementProducts",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerInvoiceNumber",
                schema: "engineer",
                table: "ContractorStatusStatementProducts");

            migrationBuilder.DropColumn(
                name: "DiscountOnInvoiceNumber",
                schema: "engineer",
                table: "ContractorStatusStatementProducts");

            migrationBuilder.DropColumn(
                name: "OtherPrice",
                schema: "engineer",
                table: "ContractorStatusStatementProducts");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "engineer",
                table: "ContractorStatusStatementProducts");

            migrationBuilder.DropColumn(
                name: "TransferPrice",
                schema: "engineer",
                table: "ContractorStatusStatementProducts");
        }
    }
}
