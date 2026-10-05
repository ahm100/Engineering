using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Addshippingcost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TransportationContractorId",
                schema: "engineer",
                table: "TransportationRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartOfContract",
                schema: "engineer",
                table: "TransportationContractors",
                type: "datetime2",
                nullable: false,
                comment: "شروع قرارداد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "LegacyId",
                schema: "engineer",
                table: "TransportationContractors",
                type: "bigint",
                nullable: true,
                comment: "شناسه قدیمی",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndOfContract",
                schema: "engineer",
                table: "TransportationContractors",
                type: "datetime2",
                nullable: false,
                comment: "پایان قرارداد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.CreateTable(
                name: "ShippingCosts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TransportationContractorId = table.Column<long>(type: "bigint", nullable: false),
                    MachineTypeId = table.Column<long>(type: "bigint", nullable: false),
                    SourceCityId = table.Column<long>(type: "bigint", nullable: false),
                    DestinationCityId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false, comment: "تعداد"),
                    LoadWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "وزن بار"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "قیمت"),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "مالیات"),
                    LegacyId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه قدیمی"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "توضیحات"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, comment: "وضعیت فعال یا غیر فعال بودن")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingCosts_MachineTypes_MachineTypeId",
                        column: x => x.MachineTypeId,
                        principalSchema: "engineer",
                        principalTable: "MachineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShippingCosts_TransportationContractors_TransportationContractorId",
                        column: x => x.TransportationContractorId,
                        principalSchema: "engineer",
                        principalTable: "TransportationContractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationRequestWarehouses",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TransportationRequestId = table.Column<long>(type: "bigint", nullable: false),
                    WarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: true),
                    ThirdPartyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackingId = table.Column<long>(type: "bigint", nullable: false),
                    PackingNumber = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingCostId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationRequestWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationRequestWarehouses_ShippingCosts_ShippingCostId",
                        column: x => x.ShippingCostId,
                        principalSchema: "engineer",
                        principalTable: "ShippingCosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportationRequestWarehouses_TransportationRequests_TransportationRequestId",
                        column: x => x.TransportationRequestId,
                        principalSchema: "engineer",
                        principalTable: "TransportationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationRequestWarehouseProducts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TransportationRequestWarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    PalletNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationRequestWarehouseProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationRequestWarehouseProducts_TransportationRequestWarehouses_TransportationRequestWarehouseId",
                        column: x => x.TransportationRequestWarehouseId,
                        principalSchema: "engineer",
                        principalTable: "TransportationRequestWarehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_TransportationContractorId",
                schema: "engineer",
                table: "TransportationRequests",
                column: "TransportationContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCosts_DestinationCityId",
                schema: "engineer",
                table: "ShippingCosts",
                column: "DestinationCityId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCosts_MachineTypeId",
                schema: "engineer",
                table: "ShippingCosts",
                column: "MachineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCosts_SourceCityId",
                schema: "engineer",
                table: "ShippingCosts",
                column: "SourceCityId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCosts_TransportationContractorId",
                schema: "engineer",
                table: "ShippingCosts",
                column: "TransportationContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestWarehouseProducts_ProductId",
                schema: "engineer",
                table: "TransportationRequestWarehouseProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestWarehouseProducts_TransportationRequestWarehouseId",
                schema: "engineer",
                table: "TransportationRequestWarehouseProducts",
                column: "TransportationRequestWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestWarehouses_PackingId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                column: "PackingId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestWarehouses_ShippingCostId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                column: "ShippingCostId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestWarehouses_ThirdPartyId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                column: "ThirdPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestWarehouses_TransportationRequestId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                column: "TransportationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestWarehouses_WarehouseId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransportationRequests_TransportationContractors_TransportationContractorId",
                schema: "engineer",
                table: "TransportationRequests",
                column: "TransportationContractorId",
                principalSchema: "engineer",
                principalTable: "TransportationContractors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransportationRequests_TransportationContractors_TransportationContractorId",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.DropTable(
                name: "TransportationRequestWarehouseProducts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "TransportationRequestWarehouses",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ShippingCosts",
                schema: "engineer");

            migrationBuilder.DropIndex(
                name: "IX_TransportationRequests_TransportationContractorId",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.DropColumn(
                name: "TransportationContractorId",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartOfContract",
                schema: "engineer",
                table: "TransportationContractors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "شروع قرارداد");

            migrationBuilder.AlterColumn<long>(
                name: "LegacyId",
                schema: "engineer",
                table: "TransportationContractors",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "شناسه قدیمی");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndOfContract",
                schema: "engineer",
                table: "TransportationContractors",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "پایان قرارداد");
        }
    }
}
