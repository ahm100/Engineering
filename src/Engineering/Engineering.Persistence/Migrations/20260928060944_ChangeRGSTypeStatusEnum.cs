using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRGSTypeStatusEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetailHistories");

            migrationBuilder.Sql(@"
                UPDATE engineer.RequestGoodsSupplyDetails
                SET Status = 1;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetails",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetailHistories",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }
    }
}
