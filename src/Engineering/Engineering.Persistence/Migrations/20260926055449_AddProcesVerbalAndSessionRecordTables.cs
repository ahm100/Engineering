using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProcesVerbalAndSessionRecordTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcesVerbals",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TitleFa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "عنوان صورت مجلس"),
                    TitleEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "عنوان صورت مجلس"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع صورت مجلس"),
                    RecordDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ صورت مجلس"),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "محل صورت مجلس"),
                    ContractId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryStatus = table.Column<int>(type: "int", nullable: true, comment: "وضعیت تحویل صورت مجلس"),
                    Limitations = table.Column<string>(type: "nvarchar(1500)", nullable: true, comment: "محدودیت های صورت مجلس"),
                    ProductStatus = table.Column<int>(type: "int", nullable: true, comment: "وضعیت تجهیز"),
                    WorkStatus = table.Column<int>(type: "int", nullable: true, comment: "وضعیت کار"),
                    LimitationStatus = table.Column<int>(type: "int", nullable: true, comment: "وضعیت رفع نواقص"),
                    WorkStartStatus = table.Column<int>(type: "int", nullable: true, comment: "وضعیت آغاز کار"),
                    WorkStopReason = table.Column<int>(type: "int", nullable: true, comment: "دلیل توقف کار"),
                    WorkStopStatus = table.Column<int>(type: "int", nullable: true, comment: "وضعیت توقف کار"),
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
                    table.PrimaryKey("PK_ProcesVerbals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcesVerbals_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "engineer",
                        principalTable: "Contracts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcesVerbals_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SessionRecords",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "عنوان"),
                    ProjectName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "نام پروژه"),
                    ContractNum = table.Column<long>(type: "bigint", nullable: true, comment: "شماره قرارداد"),
                    SessionDate = table.Column<DateOnly>(type: "date", nullable: true, comment: "تاریخ جلسه"),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false, comment: "ساعت شروع جلسه"),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false, comment: "ساعت پایان جلسه"),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "محل جلسه"),
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
                    table.PrimaryKey("PK_SessionRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcesVerbalDocs",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    URL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "شناسه مسیر فایل"),
                    ProcesVerbalId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcesVerbalDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcesVerbalDocs_ProcesVerbals_ProcesVerbalId",
                        column: x => x.ProcesVerbalId,
                        principalSchema: "engineer",
                        principalTable: "ProcesVerbals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcesVerbalEquipments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProcesVerbalId = table.Column<long>(type: "bigint", nullable: false),
                    ConsumableVolumeProductId = table.Column<long>(type: "bigint", nullable: false),
                    NewFinalValue = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "مقدار نهایی جدید"),
                    ProcesVerbalProductItemStatus = table.Column<int>(type: "int", nullable: false, comment: "وضعیت تجهیز"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true, comment: "توضیحات"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcesVerbalEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcesVerbalEquipments_ProcesVerbals_ProcesVerbalId",
                        column: x => x.ProcesVerbalId,
                        principalSchema: "engineer",
                        principalTable: "ProcesVerbals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcesVerbalEquipments_ProjectOperationDetailConsumableVolumeProducts_ConsumableVolumeProductId",
                        column: x => x.ConsumableVolumeProductId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetailConsumableVolumeProducts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcesVerbalItems",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProcesVerbalId = table.Column<long>(type: "bigint", nullable: false),
                    TitleFa = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "عنوان صورت مجلس"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcesVerbalItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcesVerbalItems_ProcesVerbals_ProcesVerbalId",
                        column: x => x.ProcesVerbalId,
                        principalSchema: "engineer",
                        principalTable: "ProcesVerbals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcesVerbalPODs",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProcesVerbalId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationDetailId = table.Column<long>(type: "bigint", nullable: false),
                    NewFinalAmount = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false, comment: "مقدار نهایی جدید"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcesVerbalPODs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcesVerbalPODs_ProcesVerbals_ProcesVerbalId",
                        column: x => x.ProcesVerbalId,
                        principalSchema: "engineer",
                        principalTable: "ProcesVerbals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcesVerbalPODs_ProjectOperationDetails_ProjectOperationDetailId",
                        column: x => x.ProjectOperationDetailId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperationDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SessionInvitees",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    SessionRecordId = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "وضعیت مدعو جلسه"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه کمپانی"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionInvitees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionInvitees_SessionRecords_SessionRecordId",
                        column: x => x.SessionRecordId,
                        principalSchema: "engineer",
                        principalTable: "SessionRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionItems",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    SessionRecordId = table.Column<long>(type: "bigint", nullable: false),
                    Descriotion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "توضیحات"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionItems_SessionRecords_SessionRecordId",
                        column: x => x.SessionRecordId,
                        principalSchema: "engineer",
                        principalTable: "SessionRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionRecordActions",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    SessionRecordId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "شرح اقدامات و تصمیمات"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "وضعیت اقدام"),
                    Deadline = table.Column<DateOnly>(type: "date", nullable: false, comment: "تاریخ سررسید"),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: "مسیول اقدام"),
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
                    table.PrimaryKey("PK_SessionRecordActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionRecordActions_SessionRecords_SessionRecordId",
                        column: x => x.SessionRecordId,
                        principalSchema: "engineer",
                        principalTable: "SessionRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionRecordDocs",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    URL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "شناسه مسیر فایل"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع پیوست جلسه"),
                    SessionRecordId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionRecordDocs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionRecordDocs_SessionRecords_SessionRecordId",
                        column: x => x.SessionRecordId,
                        principalSchema: "engineer",
                        principalTable: "SessionRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcesVerbalDocs_ProcesVerbalId",
                schema: "engineer",
                table: "ProcesVerbalDocs",
                column: "ProcesVerbalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesVerbalEquipments_ConsumableVolumeProductId",
                schema: "engineer",
                table: "ProcesVerbalEquipments",
                column: "ConsumableVolumeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesVerbalEquipments_ProcesVerbalId",
                schema: "engineer",
                table: "ProcesVerbalEquipments",
                column: "ProcesVerbalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesVerbalItems_ProcesVerbalId",
                schema: "engineer",
                table: "ProcesVerbalItems",
                column: "ProcesVerbalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesVerbalPODs_ProcesVerbalId",
                schema: "engineer",
                table: "ProcesVerbalPODs",
                column: "ProcesVerbalId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesVerbalPODs_ProjectOperationDetailId",
                schema: "engineer",
                table: "ProcesVerbalPODs",
                column: "ProjectOperationDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesVerbals_ContractId",
                schema: "engineer",
                table: "ProcesVerbals",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcesVerbals_ProjectId",
                schema: "engineer",
                table: "ProcesVerbals",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionInvitees_SessionRecordId",
                schema: "engineer",
                table: "SessionInvitees",
                column: "SessionRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionItems_SessionRecordId",
                schema: "engineer",
                table: "SessionItems",
                column: "SessionRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionRecordActions_SessionRecordId",
                schema: "engineer",
                table: "SessionRecordActions",
                column: "SessionRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_SessionRecordDocs_SessionRecordId",
                schema: "engineer",
                table: "SessionRecordDocs",
                column: "SessionRecordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcesVerbalDocs",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProcesVerbalEquipments",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProcesVerbalItems",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProcesVerbalPODs",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "SessionInvitees",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "SessionItems",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "SessionRecordActions",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "SessionRecordDocs",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProcesVerbals",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "SessionRecords",
                schema: "engineer");
        }
    }
}
