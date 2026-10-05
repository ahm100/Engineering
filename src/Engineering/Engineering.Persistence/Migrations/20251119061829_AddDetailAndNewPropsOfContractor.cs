using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailAndNewPropsOfContractor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportationRequestWarehouseProducts",
                schema: "engineer");

            migrationBuilder.AlterColumn<long>(
                name: "ShippingCostId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "PackingProductId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "PalletNumber",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TransferPrice",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TripId",
                schema: "engineer",
                table: "TransportationRequests",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "TransportationId",
                schema: "engineer",
                table: "TransportationRequests",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "engineer",
                table: "TransportationRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "engineer",
                table: "TransportationRequests",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsAggregate",
                schema: "engineer",
                table: "TransportationRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FirstPrefix",
                schema: "engineer",
                table: "TransportationContractors",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "",
                comment: "مقدار اول بارنامه داخلی");

            migrationBuilder.AddColumn<decimal>(
                name: "FixedNumber",
                schema: "engineer",
                table: "TransportationContractors",
                type: "decimal(18,2)",
                nullable: true,
                comment: "عدد ثابت");

            migrationBuilder.AddColumn<decimal>(
                name: "PercentageValue",
                schema: "engineer",
                table: "TransportationContractors",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "درصد محاسبه");

            migrationBuilder.AddColumn<string>(
                name: "SecondPrefix",
                schema: "engineer",
                table: "TransportationContractors",
                type: "nvarchar(10)",
                nullable: true,
                comment: "مقدار دوم بارنامه داخلی");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "engineer",
                table: "TransportationContractors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "نوع محاسبه پیمانکار حمل");

            migrationBuilder.AddColumn<long>(
                name: "RegionId",
                schema: "engineer",
                table: "ShippingCosts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TransportationContractorPriceWeights",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TransportationContractorId = table.Column<long>(type: "bigint", nullable: false),
                    UntilWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "تا وزن"),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false, comment: "هزینه"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "هزینه"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationContractorPriceWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationContractorPriceWeights_TransportationContractors_TransportationContractorId",
                        column: x => x.TransportationContractorId,
                        principalSchema: "engineer",
                        principalTable: "TransportationContractors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationRequestDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    GlobalFreightNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ClassifiedFreightNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransferPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InsuranceNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    InsurancePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ShippingCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductTotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OutofRange = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    TransportationRequestId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportationRequestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationRequestDetails_TransportationRequests_TransportationRequestId",
                        column: x => x.TransportationRequestId,
                        principalSchema: "engineer",
                        principalTable: "TransportationRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestWarehouses_PackingProductId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                column: "PackingProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_DestinationCityId",
                schema: "engineer",
                table: "TransportationRequests",
                column: "DestinationCityId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_DriverId",
                schema: "engineer",
                table: "TransportationRequests",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequests_StartingCityId",
                schema: "engineer",
                table: "TransportationRequests",
                column: "StartingCityId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCosts_RegionId",
                schema: "engineer",
                table: "ShippingCosts",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorPriceWeights_TransportationContractorId",
                schema: "engineer",
                table: "TransportationContractorPriceWeights",
                column: "TransportationContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestDetails_TransportationRequestId",
                schema: "engineer",
                table: "TransportationRequestDetails",
                column: "TransportationRequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportationContractorPriceWeights",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "TransportationRequestDetails",
                schema: "engineer");

            migrationBuilder.DropIndex(
                name: "IX_TransportationRequestWarehouses_PackingProductId",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_TransportationRequests_DestinationCityId",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.DropIndex(
                name: "IX_TransportationRequests_DriverId",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.DropIndex(
                name: "IX_TransportationRequests_StartingCityId",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.DropIndex(
                name: "IX_ShippingCosts_RegionId",
                schema: "engineer",
                table: "ShippingCosts");

            migrationBuilder.DropColumn(
                name: "PackingProductId",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "PalletNumber",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "TransferPrice",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "engineer",
                table: "TransportationRequestWarehouses");

            migrationBuilder.DropColumn(
                name: "IsAggregate",
                schema: "engineer",
                table: "TransportationRequests");

            migrationBuilder.DropColumn(
                name: "FirstPrefix",
                schema: "engineer",
                table: "TransportationContractors");

            migrationBuilder.DropColumn(
                name: "FixedNumber",
                schema: "engineer",
                table: "TransportationContractors");

            migrationBuilder.DropColumn(
                name: "PercentageValue",
                schema: "engineer",
                table: "TransportationContractors");

            migrationBuilder.DropColumn(
                name: "SecondPrefix",
                schema: "engineer",
                table: "TransportationContractors");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "engineer",
                table: "TransportationContractors");

            migrationBuilder.DropColumn(
                name: "RegionId",
                schema: "engineer",
                table: "ShippingCosts");

            migrationBuilder.AlterColumn<long>(
                name: "ShippingCostId",
                schema: "engineer",
                table: "TransportationRequestWarehouses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TripId",
                schema: "engineer",
                table: "TransportationRequests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TransportationId",
                schema: "engineer",
                table: "TransportationRequests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                schema: "engineer",
                table: "TransportationRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                schema: "engineer",
                table: "TransportationRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TransportationRequestWarehouseProducts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    TransportationRequestWarehouseId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    PalletNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true)
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
                name: "IX_TransportationRequestWarehouseProducts_ProductId",
                schema: "engineer",
                table: "TransportationRequestWarehouseProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationRequestWarehouseProducts_TransportationRequestWarehouseId",
                schema: "engineer",
                table: "TransportationRequestWarehouseProducts",
                column: "TransportationRequestWarehouseId");
        }
    }
}
