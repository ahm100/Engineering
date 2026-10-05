using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceNameCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceCode",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceEnName",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeviceNumber",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceCode",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "DeviceEnName",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "DeviceName",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "DeviceNumber",
                schema: "engineer",
                table: "RequestGoodsSupplies");
        }
    }
}
