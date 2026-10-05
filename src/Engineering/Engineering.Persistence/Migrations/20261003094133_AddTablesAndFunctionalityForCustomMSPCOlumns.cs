using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesAndFunctionalityForCustomMSPCOlumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Weight",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                comment: "وزن");

            migrationBuilder.CreateTable(
                name: "ProjectScheduleColumns",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectScheduleImportId = table.Column<long>(type: "bigint", nullable: false),
                    ColumnType = table.Column<int>(type: "int", nullable: false, comment: "نوع ستون"),
                    TitleFa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false, comment: "عنوان"),
                    TitleEn = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, comment: "عنوان انگلیسی"),
                    SortOrder = table.Column<int>(type: "int", nullable: false, comment: "ترتیب نمایش"),
                    DataType = table.Column<int>(type: "int", nullable: false, comment: "نوع داده"),
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
                    table.PrimaryKey("PK_ProjectScheduleColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectScheduleColumns_ProjectScheduleImports_ProjectScheduleImportId",
                        column: x => x.ProjectScheduleImportId,
                        principalSchema: "engineer",
                        principalTable: "ProjectScheduleImports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectScheduleTaskValues",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ProjectScheduleTaskId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectScheduleColumnId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true, comment: "مقدار"),
                    NumberValue = table.Column<decimal>(type: "decimal(16,2)", precision: 16, scale: 2, nullable: true, comment: "نوع عدد"),
                    DateTimeValue = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "نوع تاریخ"),
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
                    table.PrimaryKey("PK_ProjectScheduleTaskValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectScheduleTaskValues_ProjectScheduleColumns_ProjectScheduleColumnId",
                        column: x => x.ProjectScheduleColumnId,
                        principalSchema: "engineer",
                        principalTable: "ProjectScheduleColumns",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectScheduleTaskValues_ProjectScheduleTasks_ProjectScheduleTaskId",
                        column: x => x.ProjectScheduleTaskId,
                        principalSchema: "engineer",
                        principalTable: "ProjectScheduleTasks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleColumns_ProjectScheduleImportId",
                schema: "engineer",
                table: "ProjectScheduleColumns",
                column: "ProjectScheduleImportId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTaskValues_ProjectScheduleColumnId",
                schema: "engineer",
                table: "ProjectScheduleTaskValues",
                column: "ProjectScheduleColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTaskValues_ProjectScheduleTaskId",
                schema: "engineer",
                table: "ProjectScheduleTaskValues",
                column: "ProjectScheduleTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectScheduleTaskValues",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ProjectScheduleColumns",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "Weight",
                schema: "engineer",
                table: "ProjectScheduleTasks");
        }
    }
}
