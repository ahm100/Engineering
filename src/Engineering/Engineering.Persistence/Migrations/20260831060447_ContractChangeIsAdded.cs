using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ContractChangeIsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractChanges",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع تغییر قرارداد"),
                    Number = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "شماره تغییر قرارداد"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ"),
                    Subject = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "موضوع تغییر قرارداد"),
                    DurationChange = table.Column<int>(type: "int", nullable: true, comment: "تغییر مدت قرارداد"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractChanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractChanges_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "engineer",
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractChangeDocuments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractChangeId = table.Column<long>(type: "bigint", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false, comment: "مسیر فایل"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractChangeDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractChangeDocuments_ContractChanges_ContractChangeId",
                        column: x => x.ContractChangeId,
                        principalSchema: "engineer",
                        principalTable: "ContractChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractChangeItems",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractChangeId = table.Column<long>(type: "bigint", nullable: false),
                    ContractTypeDetailId = table.Column<long>(type: "bigint", nullable: true),
                    ContractTypeId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: true),
                    PricingMethod = table.Column<int>(type: "int", nullable: false, comment: "روش قیمت‌گذاری"),
                    PreviousValue = table.Column<decimal>(type: "decimal(23,5)", nullable: false, comment: "مقدار قبلی"),
                    NewValue = table.Column<decimal>(type: "decimal(23,5)", nullable: false, comment: "مقدار جدید"),
                    UnitOfMeasurementId = table.Column<long>(type: "bigint", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "مبلغ واحد"),
                    ChangeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "مبلغ تغییر"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractChangeItems", x => x.Id);
                    table.CheckConstraint("CK_ContractChangeItems_ExactlyOneOrigin", "([ContractTypeDetailId] IS NOT NULL AND [ContractTypeId] IS NULL AND [ProjectOperationDetailId] IS NULL) OR ([ContractTypeDetailId] IS NULL AND [ContractTypeId] IS NOT NULL AND [ProjectOperationDetailId] IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_ContractChangeItems_ContractChanges_ContractChangeId",
                        column: x => x.ContractChangeId,
                        principalSchema: "engineer",
                        principalTable: "ContractChanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractChangeItems_ContractTypeDetails_ContractTypeDetailId",
                        column: x => x.ContractTypeDetailId,
                        principalSchema: "engineer",
                        principalTable: "ContractTypeDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractChangeItems_ContractTypes_ContractTypeId",
                        column: x => x.ContractTypeId,
                        principalSchema: "engineer",
                        principalTable: "ContractTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractChangeItems_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractChangeDocuments_ContractChangeId",
                schema: "engineer",
                table: "ContractChangeDocuments",
                column: "ContractChangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChangeItems_ContractChangeId_ContractTypeDetailId",
                schema: "engineer",
                table: "ContractChangeItems",
                columns: new[] { "ContractChangeId", "ContractTypeDetailId" },
                unique: true,
                filter: "[IsDeleted] = 0 AND [ContractTypeDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChangeItems_ContractChangeId_ContractTypeId_ProjectOperationDetailId",
                schema: "engineer",
                table: "ContractChangeItems",
                columns: new[] { "ContractChangeId", "ContractTypeId", "ProjectOperationDetailId" },
                unique: true,
                filter: "[IsDeleted] = 0 AND [ContractTypeId] IS NOT NULL AND [ProjectOperationDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChangeItems_ContractTypeDetailId",
                schema: "engineer",
                table: "ContractChangeItems",
                column: "ContractTypeDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChangeItems_ContractTypeId",
                schema: "engineer",
                table: "ContractChangeItems",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChangeItems_ProjectOperationDetailId",
                schema: "engineer",
                table: "ContractChangeItems",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractChanges_ContractId_Number",
                schema: "engineer",
                table: "ContractChanges",
                columns: new[] { "ContractId", "Number" },
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractChangeDocuments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractChangeItems",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ContractChanges",
                schema: "engineer");
        }
    }
}
