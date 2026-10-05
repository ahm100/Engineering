using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationActionsInCalendarSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCalendarWorkingTimes_ProjectCalendarWorkingDaies_ProjectCalendarWorkingDayId",
                schema: "engineer",
                table: "ProjectCalendarWorkingTimes");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCalendarWorkingTimes_ProjectCalendarWorkingDaies_ProjectCalendarWorkingDayId",
                schema: "engineer",
                table: "ProjectCalendarWorkingTimes",
                column: "ProjectCalendarWorkingDayId",
                principalSchema: "engineer",
                principalTable: "ProjectCalendarWorkingDaies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCalendarWorkingTimes_ProjectCalendarWorkingDaies_ProjectCalendarWorkingDayId",
                schema: "engineer",
                table: "ProjectCalendarWorkingTimes");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCalendarWorkingTimes_ProjectCalendarWorkingDaies_ProjectCalendarWorkingDayId",
                schema: "engineer",
                table: "ProjectCalendarWorkingTimes",
                column: "ProjectCalendarWorkingDayId",
                principalSchema: "engineer",
                principalTable: "ProjectCalendarWorkingDaies",
                principalColumn: "Id");
        }
    }
}
