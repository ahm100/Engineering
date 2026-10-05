using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewParamsToPOHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWbses_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWbses_WbsTemplates_WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualFinishDate",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualStartDate",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BaselineDuration",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BaselineFinishDate",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BaselineStartDate",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlannedDuration",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedFinishDate",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlannedStartDate",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWbses_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectWbses",
                column: "ProjectId",
                principalSchema: "engineer",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWbses_WbsTemplates_WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbses",
                column: "WbsTemplateId",
                principalSchema: "engineer",
                principalTable: "WbsTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWbses_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectWbses_WbsTemplates_WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "ActualFinishDate",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "ActualStartDate",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "BaselineDuration",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "BaselineFinishDate",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "BaselineStartDate",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "PlannedDuration",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "PlannedFinishDate",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "PlannedStartDate",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWbses_Projects_ProjectId",
                schema: "engineer",
                table: "ProjectWbses",
                column: "ProjectId",
                principalSchema: "engineer",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectWbses_WbsTemplates_WbsTemplateId",
                schema: "engineer",
                table: "ProjectWbses",
                column: "WbsTemplateId",
                principalSchema: "engineer",
                principalTable: "WbsTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
