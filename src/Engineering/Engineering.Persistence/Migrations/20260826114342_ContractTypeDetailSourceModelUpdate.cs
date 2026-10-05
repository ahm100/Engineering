using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ContractTypeDetailSourceModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                schema: "engineer",
                table: "ContractTypeDetails",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m,
                comment: "مقدار",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)",
                oldNullable: true,
                oldComment: "مقدار");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                schema: "engineer",
                table: "ContractTypeDetails",
                type: "decimal(18,5)",
                nullable: true,
                comment: "مقدار",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)",
                oldComment: "مقدار");
        }
    }
}
