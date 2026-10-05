using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixProjectWbs : Migration
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
    }
}
