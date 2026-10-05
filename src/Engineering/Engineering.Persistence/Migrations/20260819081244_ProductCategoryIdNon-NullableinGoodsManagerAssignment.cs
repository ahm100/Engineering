using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ProductCategoryIdNonNullableinGoodsManagerAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "GoodsManagerAssignments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "دسته بندی محصول",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "دسته بندی محصول");

            migrationBuilder.AlterColumn<long>(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "دسته بندی محصول",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "دسته بندی محصول");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "GoodsManagerAssignments",
                type: "bigint",
                nullable: true,
                comment: "دسته بندی محصول",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "دسته بندی محصول");

            migrationBuilder.AlterColumn<long>(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: true,
                comment: "دسته بندی محصول",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "دسته بندی محصول");
        }
    }
}
