using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressDescriptionHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "Projects",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "توضیحات");

            migrationBuilder.AddColumn<string>(
                name: "AddressDescription",
                schema: "engineer",
                table: "Projects",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "آدرس پروژه");

            migrationBuilder.AddColumn<string>(
                name: "AddressDescription",
                schema: "engineer",
                table: "ProjectHistories",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "آدرس پروژه");

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                schema: "engineer",
                table: "ProjectHistories",
                type: "bigint",
                nullable: true,
                comment: "شناسه شهر");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "ProjectHistories",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressDescription",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AddressDescription",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "engineer",
                table: "ProjectHistories");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldNullable: true,
                oldComment: "توضیحات");
        }
    }
}
