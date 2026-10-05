using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmployerIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_OrganizationId",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                comment: "کد کارفرما",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "کد کارفرما");

            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                schema: "engineer",
                table: "ProjectHistories",
                type: "bigint",
                nullable: true,
                comment: "کد کارفرما",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "کد کارفرما");

            migrationBuilder.AddColumn<bool>(
                name: "IsOrganizationUnit",
                schema: "engineer",
                table: "ProjectHistories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "واحد سازمانی هست یا نه");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                schema: "engineer",
                table: "ProjectHistories",
                type: "bigint",
                nullable: true,
                comment: "شناسه سازمان");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOrganizationUnit",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "کد کارفرما",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "کد کارفرما");

            migrationBuilder.AlterColumn<long>(
                name: "EmployerId",
                schema: "engineer",
                table: "ProjectHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "کد کارفرما",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "کد کارفرما");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OrganizationId",
                schema: "engineer",
                table: "Projects",
                column: "OrganizationId");
        }
    }
}
