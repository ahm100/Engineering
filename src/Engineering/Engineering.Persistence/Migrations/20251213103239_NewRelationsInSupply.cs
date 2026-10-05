using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewRelationsInSupply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ConsumableVolumeProductId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ProjectProductId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProjectOperationId",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<bool>(
                name: "IsProjectSupply",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyDetails_ProjectProductId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails",
                column: "ProjectProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplies_ProjectId",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestGoodsSupplies_Projects_ProjectId",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                column: "ProjectId",
                principalSchema: "engineer",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestGoodsSupplyDetails_ProjectProducts_ProjectProductId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails",
                column: "ProjectProductId",
                principalSchema: "engineer",
                principalTable: "ProjectProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestGoodsSupplies_Projects_ProjectId",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestGoodsSupplyDetails_ProjectProducts_ProjectProductId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails");

            migrationBuilder.DropIndex(
                name: "IX_RequestGoodsSupplyDetails_ProjectProductId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails");

            migrationBuilder.DropIndex(
                name: "IX_RequestGoodsSupplies_ProjectId",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "ProjectProductId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails");

            migrationBuilder.DropColumn(
                name: "IsProjectSupply",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.AlterColumn<long>(
                name: "ConsumableVolumeProductId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProjectOperationId",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
