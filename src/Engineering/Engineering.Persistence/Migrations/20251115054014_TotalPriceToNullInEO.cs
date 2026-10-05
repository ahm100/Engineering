using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TotalPriceToNullInEO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                schema: "engineer",
                table: "EmployerOperations",
                type: "decimal(18,2)",
                nullable: true,
                comment: "قیمت کل",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "قیمت کل");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                schema: "engineer",
                table: "EmployerOperations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "قیمت کل",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true,
                oldComment: "قیمت کل");
        }
    }
}
