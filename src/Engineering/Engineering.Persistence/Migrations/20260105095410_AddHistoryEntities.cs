using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHistoryEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShippingCostHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ShippingCostId = table.Column<long>(type: "bigint", nullable: false),
                    TransportationContractorId = table.Column<long>(type: "bigint", nullable: false),
                    MachineTypeId = table.Column<long>(type: "bigint", nullable: false),
                    SourceCityId = table.Column<long>(type: "bigint", nullable: false),
                    RegionId = table.Column<long>(type: "bigint", nullable: true),
                    DestinationCityId = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: true, comment: "تعداد"),
                    LoadWeight = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "وزن بار"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "قیمت"),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "مالیات"),
                    ThirdPartyId = table.Column<long>(type: "bigint", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(18,9)", nullable: true, comment: "عرض جغرافیایی"),
                    Longitude = table.Column<decimal>(type: "decimal(18,9)", nullable: true, comment: "طول جغرافیایی"),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "از تاریخ"),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تا تاریخ"),
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
                    table.PrimaryKey("PK_ShippingCostHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingCostHistories_MachineTypes_MachineTypeId",
                        column: x => x.MachineTypeId,
                        principalSchema: "engineer",
                        principalTable: "MachineTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShippingCostHistories_ShippingCosts_ShippingCostId",
                        column: x => x.ShippingCostId,
                        principalSchema: "engineer",
                        principalTable: "ShippingCosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportationContractorPriceWeightHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TransportationContractorPriceWeightId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_TransportationContractorPriceWeightHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportationContractorPriceWeightHistories_TransportationContractorPriceWeights_TransportationContractorPriceWeightId",
                        column: x => x.TransportationContractorPriceWeightId,
                        principalSchema: "engineer",
                        principalTable: "TransportationContractorPriceWeights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCostHistories_CreatorId",
                schema: "engineer",
                table: "ShippingCostHistories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCostHistories_DestinationCityId",
                schema: "engineer",
                table: "ShippingCostHistories",
                column: "DestinationCityId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCostHistories_MachineTypeId",
                schema: "engineer",
                table: "ShippingCostHistories",
                column: "MachineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCostHistories_RegionId",
                schema: "engineer",
                table: "ShippingCostHistories",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCostHistories_ShippingCostId",
                schema: "engineer",
                table: "ShippingCostHistories",
                column: "ShippingCostId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCostHistories_SourceCityId",
                schema: "engineer",
                table: "ShippingCostHistories",
                column: "SourceCityId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingCostHistories_ThirdPartyId",
                schema: "engineer",
                table: "ShippingCostHistories",
                column: "ThirdPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorPriceWeightHistories_CreatorId",
                schema: "engineer",
                table: "TransportationContractorPriceWeightHistories",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportationContractorPriceWeightHistories_TransportationContractorPriceWeightId",
                schema: "engineer",
                table: "TransportationContractorPriceWeightHistories",
                column: "TransportationContractorPriceWeightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShippingCostHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "TransportationContractorPriceWeightHistories",
                schema: "engineer");
        }
    }
}
