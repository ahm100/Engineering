using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRGSToActivateEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplies_ParentId",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestGoodsSupplies_RequestGoodsSupplies_ParentId",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                column: "ParentId",
                principalSchema: "engineer",
                principalTable: "RequestGoodsSupplies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestGoodsSupplies_RequestGoodsSupplies_ParentId",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropIndex(
                name: "IX_RequestGoodsSupplies_ParentId",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "engineer",
                table: "RequestGoodsSupplies");
        }
    }
}
