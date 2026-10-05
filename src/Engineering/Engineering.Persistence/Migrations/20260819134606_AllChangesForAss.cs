using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AllChangesForAss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodsManagerThirdPartyId",
                schema: "engineer",
                table: "GoodsManagerAssignments");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "GoodsManagerAssignments");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                schema: "engineer",
                table: "GoodsManagerAssignments");

            migrationBuilder.DropColumn(
                name: "GoodsManagerThirdPartyId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories");

            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories");

            migrationBuilder.DropColumn(
                name: "ProductGroupId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                schema: "engineer",
                table: "GoodsManagerAssignments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "محصول",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "محصول");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                schema: "engineer",
                table: "GoodsManagerAssignments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "واحد سازمانی");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "واحد سازمانی");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "engineer",
                table: "GoodsManagerAssignments");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                schema: "engineer",
                table: "GoodsManagerAssignments",
                type: "bigint",
                nullable: true,
                comment: "محصول",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "محصول");

            migrationBuilder.AddColumn<long>(
                name: "GoodsManagerThirdPartyId",
                schema: "engineer",
                table: "GoodsManagerAssignments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "مدیر کالا");

            migrationBuilder.AddColumn<long>(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "GoodsManagerAssignments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "دسته بندی محصول");

            migrationBuilder.AddColumn<long>(
                name: "ProductGroupId",
                schema: "engineer",
                table: "GoodsManagerAssignments",
                type: "bigint",
                nullable: true,
                comment: "گروه محصول");

            migrationBuilder.AddColumn<long>(
                name: "GoodsManagerThirdPartyId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "مدیر کالا");

            migrationBuilder.AddColumn<long>(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "دسته بندی محصول");

            migrationBuilder.AddColumn<long>(
                name: "ProductGroupId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: true,
                comment: "گروه محصول");
        }
    }
}
