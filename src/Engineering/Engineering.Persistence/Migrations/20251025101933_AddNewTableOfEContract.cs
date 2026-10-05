using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTableOfEContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PreferentialReferenceCode",
                schema: "engineer",
                table: "EngineeringServices",
                type: "uniqueidentifier",
                nullable: true,
                comment: "˜Ï ãÑÌÚ ÊÝÕ?á?",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "کد مرجع تفص?ل?");

            migrationBuilder.AddColumn<long>(
                name: "EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "EmployerContractHeads",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "کد"),
                    VolumeTolerance = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "تلرانس حجمی"),
                    PriceTolerance = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "تلرانس قیمتی"),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه کارفرما"),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه ارز"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ شروع"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ پایان"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع قرارداد"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه کمپانی"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerContractHeads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerContractHeads_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerContracts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    IsFirst = table.Column<bool>(type: "bit", nullable: false, comment: "اولین قرارداد"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "وضعیت قرارداد"),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "کد"),
                    CurrencyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ شروع"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ پایان"),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "مجموع مبالغ"),
                    AdvancePayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "پیش پرداخت"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployerContractHeadId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EmployerContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerContracts_EmployerContractHeads_EmployerContractHeadId",
                        column: x => x.EmployerContractHeadId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContractHeads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerConsiderations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: false),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EmployerConsiderations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerConsiderations_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerCostOvers",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Percent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد"),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
                    CostOverId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerCostOvers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerCostOvers_CostOvers_CostOverId",
                        column: x => x.CostOverId,
                        principalSchema: "engineer",
                        principalTable: "CostOvers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerCostOvers_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerDocs",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع سند"),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ثبت"),
                    Version = table.Column<decimal>(type: "decimal(18,0)", nullable: true, comment: "ورژن"),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: false, comment: "توضیحات"),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_EmployerDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerDocs_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerOperations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "قیمت واحد"),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "قیمت کل"),
                    Description = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true, comment: "توضیحات"),
                    EmployerContractId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerOperations_EmployerContracts_EmployerContractId",
                        column: x => x.EmployerContractId,
                        principalSchema: "engineer",
                        principalTable: "EmployerContracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerOperations_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerCostOverImpacts",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Percent = table.Column<decimal>(type: "decimal(5,2)", nullable: false, comment: "درصد"),
                    ParentCostOverId = table.Column<long>(type: "bigint", nullable: false),
                    ChildCostOverId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerCostOverImpacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerCostOverImpacts_EmployerCostOvers_ChildCostOverId",
                        column: x => x.ChildCostOverId,
                        principalSchema: "engineer",
                        principalTable: "EmployerCostOvers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerCostOverImpacts_EmployerCostOvers_ParentCostOverId",
                        column: x => x.ParentCostOverId,
                        principalSchema: "engineer",
                        principalTable: "EmployerCostOvers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerDocUrls",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    URL = table.Column<string>(type: "nvarchar(1500)", nullable: false, comment: "شناسه مسیر فایل"),
                    EmployerDocId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerDocUrls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerDocUrls_EmployerDocs_EmployerDocId",
                        column: x => x.EmployerDocId,
                        principalSchema: "engineer",
                        principalTable: "EmployerDocs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerConsiderationDeps",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    EmployerConsiderationId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerOperationId = table.Column<long>(type: "bigint", nullable: false),
                    EmployerConsiderationDepId = table.Column<long>(type: "bigint", nullable: true),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerConsiderationDeps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerConsiderationDeps_EmployerConsiderationDeps_EmployerConsiderationDepId",
                        column: x => x.EmployerConsiderationDepId,
                        principalSchema: "engineer",
                        principalTable: "EmployerConsiderationDeps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerConsiderationDeps_EmployerConsiderations_EmployerConsiderationId",
                        column: x => x.EmployerConsiderationId,
                        principalSchema: "engineer",
                        principalTable: "EmployerConsiderations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerConsiderationDeps_EmployerOperations_EmployerOperationId",
                        column: x => x.EmployerOperationId,
                        principalSchema: "engineer",
                        principalTable: "EmployerOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerConsiderationDeps_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployerOperationDetails",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    EmployerOperationId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerOperationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerOperationDetails_EmployerOperations_EmployerOperationId",
                        column: x => x.EmployerOperationId,
                        principalSchema: "engineer",
                        principalTable: "EmployerOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployerOperationDetails_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployerStatusStatements_EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerConsiderationDeps_EmployerConsiderationDepId",
                schema: "engineer",
                table: "EmployerConsiderationDeps",
                column: "EmployerConsiderationDepId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerConsiderationDeps_EmployerConsiderationId",
                schema: "engineer",
                table: "EmployerConsiderationDeps",
                column: "EmployerConsiderationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerConsiderationDeps_EmployerOperationId",
                schema: "engineer",
                table: "EmployerConsiderationDeps",
                column: "EmployerOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerConsiderationDeps_ProjectOperationId",
                schema: "engineer",
                table: "EmployerConsiderationDeps",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerConsiderations_EmployerContractId",
                schema: "engineer",
                table: "EmployerConsiderations",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerContractHeads_ProjectId",
                schema: "engineer",
                table: "EmployerContractHeads",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerContracts_EmployerContractHeadId",
                schema: "engineer",
                table: "EmployerContracts",
                column: "EmployerContractHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerCostOverImpacts_ChildCostOverId",
                schema: "engineer",
                table: "EmployerCostOverImpacts",
                column: "ChildCostOverId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerCostOverImpacts_ParentCostOverId",
                schema: "engineer",
                table: "EmployerCostOverImpacts",
                column: "ParentCostOverId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerCostOvers_CostOverId",
                schema: "engineer",
                table: "EmployerCostOvers",
                column: "CostOverId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerCostOvers_EmployerContractId",
                schema: "engineer",
                table: "EmployerCostOvers",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerDocs_EmployerContractId",
                schema: "engineer",
                table: "EmployerDocs",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerDocUrls_EmployerDocId",
                schema: "engineer",
                table: "EmployerDocUrls",
                column: "EmployerDocId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperationDetails_EmployerOperationId",
                schema: "engineer",
                table: "EmployerOperationDetails",
                column: "EmployerOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperationDetails_ProjectOperationDetailId",
                schema: "engineer",
                table: "EmployerOperationDetails",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperations_EmployerContractId",
                schema: "engineer",
                table: "EmployerOperations",
                column: "EmployerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperations_ProjectOperationId",
                schema: "engineer",
                table: "EmployerOperations",
                column: "ProjectOperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerStatusStatements_EmployerContracts_EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements",
                column: "EmployerContractId",
                principalSchema: "engineer",
                principalTable: "EmployerContracts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerStatusStatements_EmployerContracts_EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements");

            migrationBuilder.DropTable(
                name: "EmployerConsiderationDeps",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerCostOverImpacts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerDocUrls",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerOperationDetails",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerConsiderations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerCostOvers",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerDocs",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerOperations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerContracts",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "EmployerContractHeads",
                schema: "engineer");

            migrationBuilder.DropIndex(
                name: "IX_EmployerStatusStatements_EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements");

            migrationBuilder.DropColumn(
                name: "EmployerContractId",
                schema: "engineer",
                table: "EmployerStatusStatements");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreferentialReferenceCode",
                schema: "engineer",
                table: "EngineeringServices",
                type: "uniqueidentifier",
                nullable: true,
                comment: "کد مرجع تفص?ل?",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "˜Ï ãÑÌÚ ÊÝÕ?á?");
        }
    }
}
