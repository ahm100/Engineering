using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusFieldForReScheduleTasks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RescheduleFromDate",
                schema: "engineer",
                table: "ProjectScheduleImports",
                type: "datetime2",
                nullable: true,
                comment: "تغییر تاریخ از");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RescheduleFromDate",
                schema: "engineer",
                table: "ProjectScheduleImports");
        }
    }
}
