using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkflowIntegration : Migration
    {
        /// <summary>جدول های اتصال مشترک و محدودیت های ثبت درخواست و پیام را ایجاد می کند.</summary>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentApprovalAttemptId",
                schema: "engineer",
                table: "CostOvers",
                type: "uniqueidentifier",
                nullable: true,
                comment: "شناسه تلاش جاری تأیید",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ApprovalStatus",
                schema: "engineer",
                table: "CostOvers",
                type: "int",
                nullable: false,
                comment: "وضعیت تأیید",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "WorkflowRequests",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "شناسه یکتای درخواست در ارتباط بین سرویس ها"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه شرکت"),
                    EntityType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "نوع موجودیت درخواست کننده"),
                    BusinessKey = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "شناسه رکورد کسب و کار"),
                    Purpose = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "هدف اجرای فرایند"),
                    WorkflowCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "کد فرایند"),
                    RequestedByUserId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه کاربر درخواست کننده"),
                    VariablesJson = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "تصویر ثابت اطلاعات اولیه فرایند"),
                    WorkflowInstanceId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه نمونه فرایند در سرویس گردش کار"),
                    DispatchStatus = table.Column<int>(type: "int", nullable: false, comment: "وضعیت ارسال درخواست"),
                    ExecutionStatus = table.Column<int>(type: "int", nullable: false, comment: "آخرین وضعیت دریافت شده از اجرای فرایند"),
                    Outcome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "نتیجه کسب و کار فرایند"),
                    RequestedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان ایجاد درخواست به وقت UTC"),
                    AcceptedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان پذیرش درخواست توسط سرویس گردش کار به وقت UTC"),
                    FinishedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان پایان اجرای فرایند به وقت UTC"),
                    LastError = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "آخرین خطای ثبت شده در پیگیری درخواست"),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false, comment: "باز بودن درخواست برای موجودیت و هدف مربوطه"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowRequests", x => x.Id);
                    table.UniqueConstraint("AK_WorkflowRequests_RequestId", x => x.RequestId);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowInbox",
                schema: "engineer",
                columns: table => new
                {
                    EventId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه یکتای رویداد در سرویس گردش کار"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه شرکت"),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "شناسه درخواست گردش کار در مهندسی"),
                    PayloadHash = table.Column<string>(type: "varchar(64)", unicode: false, maxLength: 64, nullable: false, comment: "اثر انگشت محتوای رویداد برای تشخیص تکرار متناقض"),
                    ProcessedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان ثبت نتیجه به وقت UTC")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowInbox", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_WorkflowInbox_WorkflowRequests_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "engineer",
                        principalTable: "WorkflowRequests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowOutboxes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "شناسه یکتای پیام"),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "شناسه درخواست گردش کار"),
                    CompanyId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه شرکت"),
                    MessageType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, comment: "نوع و نسخه پیام"),
                    PayloadJson = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "محتوای ثابت پیام"),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان ایجاد پیام به وقت UTC"),
                    Attempts = table.Column<int>(type: "int", nullable: false, comment: "تعداد تلاش های پردازش پیام"),
                    NextAttemptAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "زمان مجاز تلاش بعدی به وقت UTC"),
                    ProcessedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان ثبت پذیرش پیام توسط مقصد به وقت UTC"),
                    LastError = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "آخرین خطای پردازش"),
                    LockId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "شناسه مالک پردازش پیام"),
                    LockedUntilUtc = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "زمان انقضای مالکیت پردازش به وقت UTC"),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false, comment: "توقف ارسال خودکار پیام")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowOutboxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkflowOutboxes_WorkflowRequests_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "engineer",
                        principalTable: "WorkflowRequests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowInbox_RequestId",
                schema: "engineer",
                table: "WorkflowInbox",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowOutboxes_MessageId",
                schema: "engineer",
                table: "WorkflowOutboxes",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowOutboxes_NextAttemptAtUtc_CreatedAtUtc",
                schema: "engineer",
                table: "WorkflowOutboxes",
                columns: new[] { "NextAttemptAtUtc", "CreatedAtUtc" },
                filter: "[ProcessedAtUtc] IS NULL AND [IsSuspended] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowOutboxes_RequestId_MessageType",
                schema: "engineer",
                table: "WorkflowOutboxes",
                columns: new[] { "RequestId", "MessageType" },
                unique: true,
                filter: "[MessageType] = 'workflow.start.v1'");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowRequests_CompanyId_EntityType_BusinessKey_Purpose",
                schema: "engineer",
                table: "WorkflowRequests",
                columns: new[] { "CompanyId", "EntityType", "BusinessKey", "Purpose" },
                unique: true,
                filter: "[IsOpen] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowRequests_CompanyId_WorkflowInstanceId",
                schema: "engineer",
                table: "WorkflowRequests",
                columns: new[] { "CompanyId", "WorkflowInstanceId" },
                unique: true,
                filter: "[WorkflowInstanceId] IS NOT NULL");
        }

        /// <summary>جدول های اتصال را با رعایت ترتیب وابستگی ها حذف می کند.</summary>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkflowInbox",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "WorkflowOutboxes",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "WorkflowRequests",
                schema: "engineer");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentApprovalAttemptId",
                schema: "engineer",
                table: "CostOvers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "شناسه تلاش جاری تأیید");

            migrationBuilder.AlterColumn<int>(
                name: "ApprovalStatus",
                schema: "engineer",
                table: "CostOvers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "وضعیت تأیید");
        }
    }
}
