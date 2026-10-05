using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveProjectTypeFromProjectHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHistories_EngineeringProjectTypes_ProjectTypesId",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHistories_ProjectTypesId",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "ProjectTypesId",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectTypeId",
                schema: "engineer",
                table: "ProjectHistories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistories_ProjectTypeId",
                schema: "engineer",
                table: "ProjectHistories",
                column: "ProjectTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHistories_EngineeringProjectTypes_ProjectTypeId",
                schema: "engineer",
                table: "ProjectHistories",
                column: "ProjectTypeId",
                principalSchema: "engineer",
                principalTable: "EngineeringProjectTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHistories_EngineeringProjectTypes_ProjectTypeId",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.DropIndex(
                name: "IX_ProjectHistories_ProjectTypeId",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectTypeId",
                schema: "engineer",
                table: "ProjectHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProjectTypesId",
                schema: "engineer",
                table: "ProjectHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHistories_ProjectTypesId",
                schema: "engineer",
                table: "ProjectHistories",
                column: "ProjectTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHistories_EngineeringProjectTypes_ProjectTypesId",
                schema: "engineer",
                table: "ProjectHistories",
                column: "ProjectTypesId",
                principalSchema: "engineer",
                principalTable: "EngineeringProjectTypes",
                principalColumn: "Id");
        }
    }
}
