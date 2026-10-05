using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCalenderTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectCalendars",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    TitleFa = table.Column<string>(type: "nvarchar(250)", nullable: false, comment: "عنوان فارسی"),
                    TitleEn = table.Column<string>(type: "nvarchar(250)", nullable: true, comment: "عنوان انگلیسی"),
                    MppUid = table.Column<int>(type: "int", nullable: true, comment: "شناسه تقویم در Microsoft Project"),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false, comment: "تقویم پیش‌فرض پروژه"),
                    MinutesPerDay = table.Column<int>(type: "int", nullable: false, comment: "تعداد دقایق کاری استاندارد در روز"),
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
                    table.PrimaryKey("PK_ProjectCalendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCalendars_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "engineer",
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectCalendarExceptions",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ استثنا"),
                    IsWorking = table.Column<bool>(type: "bit", nullable: false, comment: "وضعیت کاری روز استثنا"),
                    From = table.Column<TimeSpan>(type: "time", nullable: true, comment: "زمان شروع کار در روز استثنا"),
                    To = table.Column<TimeSpan>(type: "time", nullable: true, comment: "زمان پایان کار در روز استثنا"),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true, comment: "توضیحات استثنای تقویم"),
                    ProjectCalendarId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ProjectCalendarExceptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCalendarExceptions_ProjectCalendars_ProjectCalendarId",
                        column: x => x.ProjectCalendarId,
                        principalSchema: "engineer",
                        principalTable: "ProjectCalendars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectCalendarWorkingDaies",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false, comment: "روز هفته"),
                    IsWorking = table.Column<bool>(type: "bit", nullable: false, comment: "روز کاری"),
                    ProjectCalendarId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ProjectCalendarWorkingDaies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCalendarWorkingDaies_ProjectCalendars_ProjectCalendarId",
                        column: x => x.ProjectCalendarId,
                        principalSchema: "engineer",
                        principalTable: "ProjectCalendars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectCalendarWorkingTimes",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    From = table.Column<TimeSpan>(type: "time", nullable: false, comment: "زمان شروع کار در روز استثنا"),
                    To = table.Column<TimeSpan>(type: "time", nullable: false, comment: "زمان پایان کار در روز استثنا"),
                    ProjectCalendarWorkingDayId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_ProjectCalendarWorkingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCalendarWorkingTimes_ProjectCalendarWorkingDaies_ProjectCalendarWorkingDayId",
                        column: x => x.ProjectCalendarWorkingDayId,
                        principalSchema: "engineer",
                        principalTable: "ProjectCalendarWorkingDaies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCalendarExceptions_ProjectCalendarId",
                schema: "engineer",
                table: "ProjectCalendarExceptions",
                column: "ProjectCalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCalendars_ProjectId",
                schema: "engineer",
                table: "ProjectCalendars",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCalendarWorkingDaies_ProjectCalendarId",
                schema: "engineer",
                table: "ProjectCalendarWorkingDaies",
                column: "ProjectCalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCalendarWorkingTimes_ProjectCalendarWorkingDayId",
                schema: "engineer",
                table: "ProjectCalendarWorkingTimes",
                column: "ProjectCalendarWorkingDayId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectCalendarExceptions",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectCalendarWorkingTimes",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectCalendarWorkingDaies",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectCalendars",
                schema: "engineer");
        }
    }
}
