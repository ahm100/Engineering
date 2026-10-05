using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractTypeAdjustmentCap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AdjustmentCapPercentage",
                schema: "engineer",
                table: "ContractTypes",
                type: "decimal(18,5)",
                nullable: true,
                comment: "درصد سقف تعدیل");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjustmentCapPercentage",
                schema: "engineer",
                table: "ContractTypes");
        }
    }
}
