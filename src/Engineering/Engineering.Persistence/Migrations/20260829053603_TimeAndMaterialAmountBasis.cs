using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TimeAndMaterialAmountBasis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeAndMaterialAmountBasis",
                schema: "engineer",
                table: "ContractTypeDetails",
                type: "int",
                nullable: true,
                comment: "مبنای محاسبه مبلغ نفر-زمان");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeAndMaterialAmountBasis",
                schema: "engineer",
                table: "ContractTypeDetails");
        }
    }
}
