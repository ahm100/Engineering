using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNameEnAndDescEn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "engineer",
                table: "Projects",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات");

            migrationBuilder.AddColumn<string>(
                name: "ProjectEnName",
                schema: "engineer",
                table: "Projects",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "نام پروژه");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "engineer",
                table: "ProjectHistories",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات");

            migrationBuilder.AddColumn<string>(
                name: "ProjectEnName",
                schema: "engineer",
                table: "ProjectHistories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "نام پروژه");

            migrationBuilder.AddColumn<string>(
                name: "CostCenterEnName",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "نام مرکز هزینه");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "engineer",
                table: "CostCenters",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات");

            migrationBuilder.AddColumn<string>(
                name: "CostCenterEnName",
                schema: "engineer",
                table: "CostCenterHistories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "نام انگلیسی مرکز هزینه");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "engineer",
                table: "CostCenterHistories",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectEnName",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "ProjectEnName",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "CostCenterEnName",
                schema: "engineer",
                table: "CostCenters");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "engineer",
                table: "CostCenters");

            migrationBuilder.DropColumn(
                name: "CostCenterEnName",
                schema: "engineer",
                table: "CostCenterHistories");

            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "engineer",
                table: "CostCenterHistories");
        }
    }
}
