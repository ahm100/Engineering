using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsForPSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PhysicalPercentComplete",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                comment: "درصد پیشرفت واقعی",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEstimated",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "bit",
                nullable: false,
                comment: "زمان تخمینی",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                comment: "هزینه");

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ سررسید");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true,
                comment: "یاداشت");

            migrationBuilder.AddColumn<long>(
                name: "RemainingDurationMinutes",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "bigint",
                nullable: true,
                comment: "زمان کاری باقی مانده");

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusDate",
                schema: "engineer",
                table: "ProjectScheduleImports",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ وضعیتی");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.DropColumn(
                name: "Deadline",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.DropColumn(
                name: "Note",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.DropColumn(
                name: "RemainingDurationMinutes",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.DropColumn(
                name: "StatusDate",
                schema: "engineer",
                table: "ProjectScheduleImports");

            migrationBuilder.AlterColumn<decimal>(
                name: "PhysicalPercentComplete",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldComment: "درصد پیشرفت واقعی");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEstimated",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "زمان تخمینی");
        }
    }
}
