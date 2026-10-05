using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCalendarRelationIntoTaskMSP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CalendarId",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectScheduleTasks_CalendarId",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                column: "CalendarId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectScheduleTasks_ProjectCalendars_CalendarId",
                schema: "engineer",
                table: "ProjectScheduleTasks",
                column: "CalendarId",
                principalSchema: "engineer",
                principalTable: "ProjectCalendars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectScheduleTasks_ProjectCalendars_CalendarId",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.DropIndex(
                name: "IX_ProjectScheduleTasks_CalendarId",
                schema: "engineer",
                table: "ProjectScheduleTasks");

            migrationBuilder.DropColumn(
                name: "CalendarId",
                schema: "engineer",
                table: "ProjectScheduleTasks");
        }
    }
}
