using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProjectCostCenterRequestTableAddParameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestedCostCenterName",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "نام مرکز هزینه درخواستی",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldComment: "نام مرکز هزینه درخواستی");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: false,
                defaultValue: "",
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500,
                oldNullable: true,
                oldComment: "توضیحات");

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                comment: "دلیل رد درخواست");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectionReason",
                schema: "engineer",
                table: "ProjectCostCenterRequests");

            migrationBuilder.AlterColumn<string>(
                name: "RequestedCostCenterName",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "نام مرکز هزینه درخواستی",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true,
                oldComment: "نام مرکز هزینه درخواستی");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "ProjectCostCenterRequests",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldMaxLength: 1500,
                oldComment: "توضیحات");
        }
    }
}
