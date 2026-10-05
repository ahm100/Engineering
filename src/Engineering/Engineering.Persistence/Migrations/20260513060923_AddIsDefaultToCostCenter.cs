using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDefaultToCostCenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "engineer",
                table: "CostCenters",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "پیش‌فرض بودن");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "engineer",
                table: "CostCenterHistories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "پیش‌فرض بودن");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "engineer",
                table: "CostCenters");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "engineer",
                table: "CostCenterHistories");
        }
    }
}
