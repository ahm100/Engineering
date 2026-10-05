using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RGSType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyTypes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Importance = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    DelivaryDeadLine = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferenceId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه محصول سفارش داده شده"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع محصول سفارش داده شده"),
                    PackageId = table.Column<long>(type: "bigint", nullable: true),
                    RequestedCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PackageCount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PackageUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PackingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CheckGroup = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContractorId = table.Column<long>(type: "bigint", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ManagementDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    LastDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
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
                    table.PrimaryKey("PK_RequestGoodsSupplyTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyTypes_RequestGoodsSupplies_RequestGoodsSupplyId",
                        column: x => x.RequestGoodsSupplyId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyTypeDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Importance = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ReferenceId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه محصول سفارش داده شده"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع محصول سفارش داده شده"),
                    RequestedCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DelivaryDeadLine = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PackingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ManagementDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    CheckGroup = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContractorId = table.Column<long>(type: "bigint", nullable: true),
                    PackageId = table.Column<long>(type: "bigint", nullable: true),
                    PackageCount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PackageUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ProjectProductId = table.Column<long>(type: "bigint", nullable: true),
                    RequestGoodsSupplyId = table.Column<long>(type: "bigint", nullable: false),
                    RequestGoodsSupplyTypeId = table.Column<long>(type: "bigint", nullable: true),
                    CostCenterId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyTypeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyTypeDetails_CostCenters_CostCenterId",
                        column: x => x.CostCenterId,
                        principalSchema: "engineer",
                        principalTable: "CostCenters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyTypeDetails_ProjectProducts_ProjectProductId",
                        column: x => x.ProjectProductId,
                        principalSchema: "engineer",
                        principalTable: "ProjectProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyTypeDetails_RequestGoodsSupplies_RequestGoodsSupplyId",
                        column: x => x.RequestGoodsSupplyId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyTypeDetails_RequestGoodsSupplyTypes_RequestGoodsSupplyTypeId",
                        column: x => x.RequestGoodsSupplyTypeId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyTypeDocument",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestGoodsSupplyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyTypeDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyTypeDocument_RequestGoodsSupplyTypes_RequestGoodsSupplyTypeId",
                        column: x => x.RequestGoodsSupplyTypeId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyTypeHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Importance = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    DelivaryDeadLine = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReferenceId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه محصول سفارش داده شده"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع محصول سفارش داده شده"),
                    PackageId = table.Column<long>(type: "bigint", nullable: true),
                    RequestedCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TaxPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    TaxNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountByPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    DiscountByNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DiscountedPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransferPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PackageCount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PackageUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PackingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CheckGroup = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContractorId = table.Column<long>(type: "bigint", nullable: true),
                    DestinationWarehouseId = table.Column<long>(type: "bigint", nullable: true),
                    CustomerInvoiceNumber = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ManagementDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    LastDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    RequestGoodsSupplyTypeId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyTypeHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyTypeHistories_RequestGoodsSupplyTypes_RequestGoodsSupplyTypeId",
                        column: x => x.RequestGoodsSupplyTypeId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyTypeDetailDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Url = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    RequestGoodsSupplyTypeDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyTypeDetailDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyTypeDetailDocuments_RequestGoodsSupplyTypeDetails_RequestGoodsSupplyTypeDetailId",
                        column: x => x.RequestGoodsSupplyTypeDetailId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplyTypeDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RequestGoodsSupplyTypeDetailHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Importance = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ReferenceId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه محصول سفارش داده شده"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع محصول سفارش داده شده"),
                    RequestedCount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DelivaryDeadLine = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PackingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ManagementDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    CheckGroup = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ContractorId = table.Column<long>(type: "bigint", nullable: true),
                    PackageId = table.Column<long>(type: "bigint", nullable: true),
                    PackageCount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PackageUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastDescription = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    RequestGoodsSupplyTypeDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestGoodsSupplyTypeDetailHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestGoodsSupplyTypeDetailHistories_RequestGoodsSupplyTypeDetails_RequestGoodsSupplyTypeDetailId",
                        column: x => x.RequestGoodsSupplyTypeDetailId,
                        principalSchema: "engineer",
                        principalTable: "RequestGoodsSupplyTypeDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyTypeDetailDocuments_RequestGoodsSupplyTypeDetailId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetailDocuments",
                column: "RequestGoodsSupplyTypeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyTypeDetailHistories_RequestGoodsSupplyTypeDetailId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetailHistories",
                column: "RequestGoodsSupplyTypeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyTypeDetails_CostCenterId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetails",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyTypeDetails_ProjectProductId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetails",
                column: "ProjectProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyTypeDetails_RequestGoodsSupplyId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetails",
                column: "RequestGoodsSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyTypeDetails_RequestGoodsSupplyTypeId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetails",
                column: "RequestGoodsSupplyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyTypeDocument_RequestGoodsSupplyTypeId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDocument",
                column: "RequestGoodsSupplyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyTypeHistories_RequestGoodsSupplyTypeId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeHistories",
                column: "RequestGoodsSupplyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestGoodsSupplyTypes_RequestGoodsSupplyId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypes",
                column: "RequestGoodsSupplyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyTypeDetailDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyTypeDetailHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyTypeDocument",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyTypeHistories",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyTypeDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "RequestGoodsSupplyTypes",
                schema: "engineer");
        }
    }
}
