using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressAndUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConsumptionAddress",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConsumptionRateAndInventoryUrl",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConsumptionAddress",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "ConsumptionRateAndInventoryUrl",
                schema: "engineer",
                table: "RequestGoodsSupplies");
        }
    }
}
