using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewFieldsToProjectWbsAndTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProjectScheduleTasks_ProjectScheduleImportId_MppUid",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.AlterColumn<int>(
                name: "MppUid",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "int",
                nullable: true,
                comment: "شناسه یکتای فعالیت در Microsoft Project",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "شناسه یکتای فعالیت در Microsoft Project");

            migrationBuilder.AlterColumn<int>(
                name: "MppId",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "int",
                nullable: true,
                comment: "شناسه فعالیت در Microsoft Project",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "شناسه فعالیت در Microsoft Project");

            migrationBuilder.AddColumn<bool>(
                name: "IsEstimated",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                schema: "engineer",
                table: "ProjectScheduleImports",
                type: "nvarchar(250)",
                nullable: true,
                comment: "عنوان فارسی",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldComment: "عنوان فارسی");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileId",
                schema: "engineer",
                table: "ProjectScheduleImports",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduleStartDate",
                schema: "engineer",
                table: "ProjectScheduleImports",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTasks_ParentId",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTasks_ProjectScheduleImportId_MppUid",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                columns: new[] { "ProjectScheduleImportId", "MppUid" },
                unique: true,
                filter: "[MppUid] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectScheduleTasks_ProjectScheduleTasks_ParentId",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                column: "ParentId",
                principalSchema: "engineer",
                principalTable: "ProjectScheduleTasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectScheduleTasks_ProjectScheduleTasks_ParentId",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectScheduleTasks_ParentId",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectScheduleTasks_ProjectScheduleImportId_MppUid",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.DropColumn(
                name: "IsEstimated",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.DropColumn(
                name: "ParentId",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.DropColumn(
                name: "ScheduleStartDate",
                schema: "engineer",
                table: "ProjectScheduleImports");

            migrationBuilder.AlterColumn<int>(
                name: "MppUid",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "شناسه یکتای فعالیت در Microsoft Project",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "شناسه یکتای فعالیت در Microsoft Project");

            migrationBuilder.AlterColumn<int>(
                name: "MppId",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "شناسه فعالیت در Microsoft Project",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "شناسه فعالیت در Microsoft Project");

            migrationBuilder.AlterColumn<string>(
                name: "FileName",
                schema: "engineer",
                table: "ProjectScheduleImports",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "",
                comment: "عنوان فارسی",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldNullable: true,
                oldComment: "عنوان فارسی");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileId",
                schema: "engineer",
                table: "ProjectScheduleImports",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTasks_ProjectScheduleImportId_MppUid",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                columns: new[] { "ProjectScheduleImportId", "MppUid" },
                unique: true);
        }
    }
}
