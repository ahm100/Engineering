using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractTypeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractTypeDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractTypeId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه نوع قرارداد"),
                    ConsumableVolumeProductId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه حجم مصرفی کالا"),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه برآورد"),
                    ProjectOperationDetailContractorServiceId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه خدمت پیمانکار ریزمتره"),
                    Quantity = table.Column<decimal>(type: "decimal(18,5)", nullable: false, comment: "مقدار"),
                    UnitOfMeasurementId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه واحد اندازه‌گیری"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "مبلغ واحد"),
                    FixedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "مبلغ مقطوع"),
                    TechnicalSpecifications = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "مشخصات فنی"),
                    ExpectedDeliverables = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "خروجی‌های مورد انتظار"),
                    Duration = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "مدت"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypeDetails", x => x.Id);
                    table.CheckConstraint("CK_ContractTypeDetails_ExactlyOneSource", "(CASE WHEN [ConsumableVolumeProductId] IS NULL THEN 0 ELSE 1 END + CASE WHEN [ProjectOperationDetailId] IS NULL THEN 0 ELSE 1 END + CASE WHEN [ProjectOperationDetailContractorServiceId] IS NULL THEN 0 ELSE 1 END) = 1");
                    table.ForeignKey(
                        name: "FK_ContractTypeDetails_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalSchema: "engineer",
                        principalTable: "ContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractTypeDetails_ProjectOperationDetailConsumableVolumeProducts_ConsumableVolumeProductId",
                        column: x => x.ConsumableVolumeProductId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailConsumableVolumeProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractTypeDetails_ProjectOperationDetailContractorServices_ProjectOperationDetailContractorServiceId",
                        column: x => x.ProjectOperationDetailContractorServiceId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailContractorServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractTypeDetails_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeDetails_ConsumableVolumeProductId",
                schema: "engineer",
                table: "ContractTypeDetails",
                column: "ConsumableVolumeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeDetails_ContractTypeId_ConsumableVolumeProductId",
                schema: "engineer",
                table: "ContractTypeDetails",
                columns: new[] { "ContractTypeId", "ConsumableVolumeProductId" },
                unique: true,
                filter: "[IsDeleted] = 0 AND [ConsumableVolumeProductId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeDetails_ContractTypeId_ProjectOperationDetailContractorServiceId",
                schema: "engineer",
                table: "ContractTypeDetails",
                columns: new[] { "ContractTypeId", "ProjectOperationDetailContractorServiceId" },
                unique: true,
                filter: "[IsDeleted] = 0 AND [ProjectOperationDetailContractorServiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeDetails_ContractTypeId_ProjectOperationDetailId",
                schema: "engineer",
                table: "ContractTypeDetails",
                columns: new[] { "ContractTypeId", "ProjectOperationDetailId" },
                unique: true,
                filter: "[IsDeleted] = 0 AND [ProjectOperationDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeDetails_ProjectOperationDetailContractorServiceId",
                schema: "engineer",
                table: "ContractTypeDetails",
                column: "ProjectOperationDetailContractorServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeDetails_ProjectOperationDetailId",
                schema: "engineer",
                table: "ContractTypeDetails",
                column: "ProjectOperationDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractTypeDetails",
                schema: "engineer");
        }
    }
}
