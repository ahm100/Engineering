using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MakeNullCategoryInProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "LastDescription",
                schema: "engineer",
                table: "FiduciaryProducts",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RequestNumber",
                schema: "engineer",
                table: "FiduciaryProducts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusDescription",
                schema: "engineer",
                table: "FiduciaryProducts",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastDescription",
                schema: "engineer",
                table: "FiduciaryProductHistories",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusDescription",
                schema: "engineer",
                table: "FiduciaryProductHistories",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastDescription",
                schema: "engineer",
                table: "FiduciaryProductDetails",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusDescription",
                schema: "engineer",
                table: "FiduciaryProductDetails",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastDescription",
                schema: "engineer",
                table: "FiduciaryProductDetailHistories",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StatusDescription",
                schema: "engineer",
                table: "FiduciaryProductDetailHistories",
                type: "nvarchar(1500)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastDescription",
                schema: "engineer",
                table: "FiduciaryProducts");

            migrationBuilder.DropColumn(
                name: "RequestNumber",
                schema: "engineer",
                table: "FiduciaryProducts");

            migrationBuilder.DropColumn(
                name: "StatusDescription",
                schema: "engineer",
                table: "FiduciaryProducts");

            migrationBuilder.DropColumn(
                name: "LastDescription",
                schema: "engineer",
                table: "FiduciaryProductHistories");

            migrationBuilder.DropColumn(
                name: "StatusDescription",
                schema: "engineer",
                table: "FiduciaryProductHistories");

            migrationBuilder.DropColumn(
                name: "LastDescription",
                schema: "engineer",
                table: "FiduciaryProductDetails");

            migrationBuilder.DropColumn(
                name: "StatusDescription",
                schema: "engineer",
                table: "FiduciaryProductDetails");

            migrationBuilder.DropColumn(
                name: "LastDescription",
                schema: "engineer",
                table: "FiduciaryProductDetailHistories");

            migrationBuilder.DropColumn(
                name: "StatusDescription",
                schema: "engineer",
                table: "FiduciaryProductDetailHistories");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
