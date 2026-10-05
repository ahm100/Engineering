using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewParamToRGS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDeadline",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationNumber",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RequestingOrganizationId",
                schema: "engineer",
                table: "RequestGoodsSupplies",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    RequestGoodsSupplyId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyDocuments_RequestGoodsSupplies_RequestGoodsSupplyId",
                        column: x => x.RequestGoodsSupplyId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyDetails_CostCenterId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyDocuments_RequestGoodsSupplyId",
                schema: "engineer",
                table: "RequestGoodsSupplyDocuments",
                column: "RequestGoodsSupplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestGoodsSupplyDetails_CostCenters_CostCenterId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails",
                column: "CostCenterId",
                principalSchema: "engineer",
                principalTable: "CostCenters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestGoodsSupplyDetails_CostCenters_CostCenterId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyDocuments",
                schema: "engineer");

            migrationBuilder.DropIndex(
                name: "IX_RequestGoodsSupplyDetails_CostCenterId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                schema: "engineer",
                table: "RequestGoodsSupplyDetails");

            migrationBuilder.DropColumn(
                name: "DeliveryDeadline",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "RegistrationNumber",
                schema: "engineer",
                table: "RequestGoodsSupplies");

            migrationBuilder.DropColumn(
                name: "RequestingOrganizationId",
                schema: "engineer",
                table: "RequestGoodsSupplies");
        }
    }
}
