using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ContractEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RegisteredInitialAmount",
                schema: "engineer",
                table: "ContractFinancialInformations",
                type: "decimal(18,2)",
                nullable: true,
                comment: "مبلغ اولیه ثبت‌شده قرارداد");

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                schema: "engineer",
                table: "ContractChanges",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "شیوه ثبت تغییر قرارداد");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredInitialAmount",
                schema: "engineer",
                table: "ContractFinancialInformations");

            migrationBuilder.DropColumn(
                name: "Mode",
                schema: "engineer",
                table: "ContractChanges");
        }
    }
}
