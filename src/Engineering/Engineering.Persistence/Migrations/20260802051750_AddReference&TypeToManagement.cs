using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddReferenceTypeToManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReferenceId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailManagements",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RequestGoodsSupplyTypeId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailManagements",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyDetailManagements_RequestGoodsSupplyTypeId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailManagements",
                column: "RequestGoodsSupplyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestGoodsSupplyDetailManagements_RequestGoodsSupplyTypes_RequestGoodsSupplyTypeId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailManagements",
                column: "RequestGoodsSupplyTypeId",
                principalSchema: "engineer",
                principalTable: "RequestGoodsSupplyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(@"
                UPDATE engineer.RequestGoodsSupplyDetailManagements
                SET ReferenceId = ProductId
                WHERE ProductId IS NOT NULL;
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestGoodsSupplyDetailManagements_RequestGoodsSupplyTypes_RequestGoodsSupplyTypeId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailManagements");

            migrationBuilder.DropIndex(
                name: "IX_RequestGoodsSupplyDetailManagements_RequestGoodsSupplyTypeId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailManagements");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailManagements");

            migrationBuilder.DropColumn(
                name: "RequestGoodsSupplyTypeId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetailManagements");
        }
    }
}
