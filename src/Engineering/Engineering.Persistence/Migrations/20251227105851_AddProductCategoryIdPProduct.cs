using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProductCategoryIdPProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ProductGroupId",
                schema: "engineer",
                table: "ProjectProducts",
                type: "bigint",
                nullable: true,
                comment: "شناسه گروه کالا",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه گروه کالا");

            migrationBuilder.AddColumn<long>(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "ProjectProducts",
                type: "bigint",
                nullable: true,
                comment: "شناسه دسته بندی کالا");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "ProjectProducts");

            migrationBuilder.AlterColumn<long>(
                name: "ProductGroupId",
                schema: "engineer",
                table: "ProjectProducts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "شناسه گروه کالا",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "شناسه گروه کالا");
        }
    }
}
