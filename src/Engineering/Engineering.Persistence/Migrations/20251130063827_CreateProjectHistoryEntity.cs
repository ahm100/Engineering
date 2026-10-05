using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateProjectHistoryEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SupervisorEngineer",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                comment: "مهندس ناظر",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "engineer",
                table: "Projects",
                type: "int",
                nullable: false,
                comment: "وضعیت پروژه",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                schema: "engineer",
                table: "Projects",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                comment: "نام پروژه",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<long>(
                name: "ProjectManager",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: false,
                comment: "مدیر پروژه",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectCode",
                schema: "engineer",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "کد نوع پروژه",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PlanningAssistant",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                comment: "مسئول برنامه ریزی",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: false,
                comment: "کد کارفرما",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<bool>(
                name: "Contractual",
                schema: "engineer",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: true,
                comment: "نوع قرارداد",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                comment: "شناسه کمپانی",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "Advisor",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                comment: "مشاور پروژه",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AddAutomated",
                schema: "engineer",
                table: "Projects",
                type: "bit",
                nullable: false,
                comment: "افزوده‌شده خودکار",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "ProjectHistories",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "نام پروژه"),
                    ProjectCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, comment: "کد نوع پروژه"),
                    EmployerId = table.Column<long>(type: "bigint", nullable: false, comment: "کد کارفرما"),
                    SupervisorEngineer = table.Column<long>(type: "bigint", nullable: true, comment: "مهندس ناظر"),
                    Advisor = table.Column<long>(type: "bigint", nullable: true, comment: "مشاور پروژه"),
                    ProjectManager = table.Column<long>(type: "bigint", nullable: false, comment: "مدیر پروژه"),
                    PlanningAssistant = table.Column<long>(type: "bigint", nullable: true, comment: "مسئول برنامه ریزی"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "وضعیت پروژه"),
                    AddAutomated = table.Column<bool>(type: "bit", nullable: false, comment: "افزوده‌شده خودکار"),
                    Contractual = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "نوع قرارداد"),
                    CollectiveService = table.Column<bool>(type: "bit", nullable: false),
                    CompanyId = table.Column<long>(type: "bigint", nullable: true, comment: "شناسه کمپانی"),
                    PreferentialReferenceCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "کد مرجع تفصیلی"),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectTypesId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectTypeId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ProjectHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectHistories_EngineeringProjectTypes_ProjectTypesId",
                        column: x => x.ProjectTypesId,
                        principalSchema: "engineer",
                        principalTable: "EngineeringProjectTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectHistories_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistories_ProjectId",
                schema: "engineer",
                table: "ProjectHistories",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistories_ProjectTypesId",
                schema: "engineer",
                table: "ProjectHistories",
                column: "ProjectTypesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectHistories",
                schema: "engineer");

            migrationBuilder.AlterColumn<long>(
                name: "SupervisorEngineer",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "مهندس ناظر");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "engineer",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "وضعیت پروژه");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectName",
                schema: "engineer",
                table: "Projects",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "نام پروژه");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectManager",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "مدیر پروژه");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectCode",
                schema: "engineer",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "کد نوع پروژه");

            migrationBuilder.AlterColumn<long>(
                name: "PlanningAssistant",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "مسئول برنامه ریزی");

            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "کد کارفرما");

            migrationBuilder.AlterColumn<bool>(
                name: "Contractual",
                schema: "engineer",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true,
                oldComment: "نوع قرارداد");

            migrationBuilder.AlterColumn<long>(
                name: "CompanyId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "شناسه کمپانی");

            migrationBuilder.AlterColumn<long>(
                name: "Advisor",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "مشاور پروژه");

            migrationBuilder.AlterColumn<bool>(
                name: "AddAutomated",
                schema: "engineer",
                table: "Projects",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "افزوده‌شده خودکار");
        }
    }
}
