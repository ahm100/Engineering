using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddWbsAndCalenderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.AlterColumn<long>(
                name: "WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "engineer",
                table: "ProjectWbses",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "",
                comment: "کد");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "engineer",
                table: "ProjectWbses",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات انگلیسی");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionFa",
                schema: "engineer",
                table: "ProjectWbses",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات فارسی");

            migrationBuilder.AddColumn<int>(
                name: "MppUid",
                schema: "engineer",
                table: "ProjectWbses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutlineLevel",
                schema: "engineer",
                table: "ProjectWbses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutlineNumber",
                schema: "engineer",
                table: "ProjectWbses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectScheduleImportId",
                schema: "engineer",
                table: "ProjectWbses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                schema: "engineer",
                table: "ProjectWbses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TitleEn",
                schema: "engineer",
                table: "ProjectWbses",
                type: "nvarchar(250)",
                nullable: true,
                comment: "عنوان انگلیسی");

            migrationBuilder.AddColumn<string>(
                name: "TitleFa",
                schema: "engineer",
                table: "ProjectWbses",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "",
                comment: "عنوان فارسی");

            migrationBuilder.CreateTable(
                name: "ProjectScheduleImports",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(250)", nullable: false, comment: "عنوان فارسی"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "وضعیت های بارگذاری فایل"),
                    ImportedAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ورود"),
                    ImportedBy = table.Column<long>(type: "bigint", nullable: false, comment: "کاربر واردکننده"),
                    ErrorMessage = table.Column<string>(type: "nvarchar(2500)", nullable: true, comment: "پیام خطای ورود"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ProjectScheduleImports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectScheduleImports_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectScheduleTasks",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "عنوان"),
                    MppUid = table.Column<int>(type: "int", nullable: false, comment: "شناسه یکتای فعالیت در Microsoft Project"),
                    MppId = table.Column<int>(type: "int", nullable: false, comment: "شناسه فعالیت در Microsoft Project"),
                    SortOrder = table.Column<int>(type: "int", nullable: false, comment: "ترتیب نمایش"),
                    OutlineLevel = table.Column<int>(type: "int", nullable: false, comment: "سطح ساختاری"),
                    OutlineNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "شماره ساختاری"),
                    PlannedStart = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ شروع برنامه‌ریزی‌شده"),
                    PlannedFinish = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ پایان برنامه‌ریزی‌شده"),
                    PlannedDurationMinutes = table.Column<long>(type: "bigint", nullable: true, comment: "مدت زمان برنامه‌ریزی‌شده به دقیقه"),
                    PercentComplete = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false, comment: "درصد پیشرفت"),
                    BaselineStart = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ شروع خط مبنا"),
                    BaselineFinish = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ پایان خط مبنا"),
                    BaselineDurationMinutes = table.Column<long>(type: "bigint", nullable: true, comment: "مدت زمان خط مبنا به دقیقه"),
                    ActualStart = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ شروع واقعی"),
                    ActualFinish = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ پایان واقعی"),
                    ActualDurationMinutes = table.Column<long>(type: "bigint", nullable: true, comment: "مدت زمان واقعی به دقیقه"),
                    IsMilestone = table.Column<bool>(type: "bit", nullable: false, comment: "نقطه عطف"),
                    IsCritical = table.Column<bool>(type: "bit", nullable: false, comment: "فعالیت بحرانی"),
                    ProjectScheduleImportId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectWbsId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_ProjectScheduleTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectScheduleTasks_ProjectScheduleImports_ProjectScheduleImportId",
                        column: x => x.ProjectScheduleImportId,
                        principalSchema: "engineer",
                        principalTable: "ProjectScheduleImports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectScheduleTasks_ProjectWbses_ProjectWbsId",
                        column: x => x.ProjectWbsId,
                        principalSchema: "engineer",
                        principalTable: "ProjectWbses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectScheduleTaskDependencies",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع های وابستگی"),
                    LagMinutes = table.Column<long>(type: "bigint", nullable: false, comment: "تأخیر زمانی به دقیقه"),
                    PredecessorTaskId = table.Column<long>(type: "bigint", nullable: false),
                    SuccessorTaskId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ProjectScheduleTaskDependencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectScheduleTaskDependencies_ProjectScheduleTasks_PredecessorTaskId",
                        column: x => x.PredecessorTaskId,
                        principalSchema: "engineer",
                        principalTable: "ProjectScheduleTasks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectScheduleTaskDependencies_ProjectScheduleTasks_SuccessorTaskId",
                        column: x => x.SuccessorTaskId,
                        principalSchema: "engineer",
                        principalTable: "ProjectScheduleTasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectScheduleTaskOperations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectScheduleTaskId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectOperationId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ProjectScheduleTaskOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectScheduleTaskOperations_ProjectOperations_ProjectOperationId",
                        column: x => x.ProjectOperationId,
                        principalSchema: "engineer",
                        principalTable: "ProjectOperations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectScheduleTaskOperations_ProjectScheduleTasks_ProjectScheduleTaskId",
                        column: x => x.ProjectScheduleTaskId,
                        principalSchema: "engineer",
                        principalTable: "ProjectScheduleTasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWbses_ProjectScheduleImportId",
                schema: "engineer",
                table: "ProjectWbses",
                column: "ProjectScheduleImportId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleImports_ProjectId",
                schema: "engineer",
                table: "ProjectScheduleImports",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTaskDependencies_PredecessorTaskId",
                schema: "engineer",
                table: "ProjectScheduleTaskDependencies",
                column: "PredecessorTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTaskDependencies_SuccessorTaskId",
                schema: "engineer",
                table: "ProjectScheduleTaskDependencies",
                column: "SuccessorTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTaskOperations_ProjectOperationId",
                schema: "engineer",
                table: "ProjectScheduleTaskOperations",
                column: "ProjectOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTaskOperations_ProjectScheduleTaskId",
                schema: "engineer",
                table: "ProjectScheduleTaskOperations",
                column: "ProjectScheduleTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTasks_ProjectScheduleImportId_MppUid",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                columns: new[] { "ProjectScheduleImportId", "MppUid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTasks_ProjectWbsId",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                column: "ProjectWbsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWbses_ProjectScheduleImports_ProjectScheduleImportId",
                schema: "engineer",
                table: "ProjectWbses",
                column: "ProjectScheduleImportId",
                principalSchema: "engineer",
                principalTable: "ProjectScheduleImports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWbses_ProjectScheduleImports_ProjectScheduleImportId",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropTable(
                name: "ProjectScheduleTaskDependencies",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectScheduleTaskOperations",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectScheduleTasks",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectScheduleImports",
                schema: "engineer");

            migrationBuilder.DropIndex(
                name: "IX_ProjectWbses_ProjectScheduleImportId",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "DescriptionFa",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "MppUid",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "OutlineLevel",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "OutlineNumber",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "ProjectScheduleImportId",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "SortOrder",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "TitleEn",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "TitleFa",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.AlterColumn<long>(
                name: "WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "ProjectWbses",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات");
        }
    }
}
