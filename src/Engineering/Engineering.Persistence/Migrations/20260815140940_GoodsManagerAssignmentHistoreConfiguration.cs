using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GoodsManagerAssignmentHistoreConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsManagerAssignmentHistory_GoodsManagerAssignments_GoodsManagerAssignmentId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoodsManagerAssignmentHistory",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory");

            migrationBuilder.RenameTable(
                name: "GoodsManagerAssignmentHistory",
                schema: "engineer",
                newName: "GoodsManagerAssignmentHistories",
                newSchema: "engineer");

            migrationBuilder.RenameIndex(
                name: "IX_GoodsManagerAssignmentHistory_GoodsManagerAssignmentId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                newName: "IX_GoodsManagerAssignmentHistories_GoodsManagerAssignmentId");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: true,
                comment: "ویرایش کننده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "datetime2",
                nullable: true,
                comment: "تاریخ ویرایش",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: true,
                comment: "محصول",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProductGroupId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: true,
                comment: "گروه محصول",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: true,
                comment: "دسته بندی محصول",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bit",
                nullable: false,
                comment: "حذف شدگی",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<long>(
                name: "GoodsManagerThirdPartyId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: false,
                comment: "مدیر کالا",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "nvarchar(1500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: false,
                comment: "ایجاد کننده",
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "datetime2",
                nullable: false,
                comment: "تاریخ ایجاد",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                type: "bigint",
                nullable: false,
                comment: "شناسه",
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "2, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoodsManagerAssignmentHistories",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsManagerAssignmentHistories_GoodsManagerAssignments_GoodsManagerAssignmentId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories",
                column: "GoodsManagerAssignmentId",
                principalSchema: "engineer",
                principalTable: "GoodsManagerAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsManagerAssignmentHistories_GoodsManagerAssignments_GoodsManagerAssignmentId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GoodsManagerAssignmentHistories",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistories");

            migrationBuilder.RenameTable(
                name: "GoodsManagerAssignmentHistories",
                schema: "engineer",
                newName: "GoodsManagerAssignmentHistory",
                newSchema: "engineer");

            migrationBuilder.RenameIndex(
                name: "IX_GoodsManagerAssignmentHistories_GoodsManagerAssignmentId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                newName: "IX_GoodsManagerAssignmentHistory_GoodsManagerAssignmentId");

            migrationBuilder.AlterColumn<long>(
                name: "UpdaterId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "ویرایش کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldComment: "تاریخ ویرایش");

            migrationBuilder.AlterColumn<long>(
                name: "ProductId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "محصول");

            migrationBuilder.AlterColumn<long>(
                name: "ProductGroupId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "گروه محصول");

            migrationBuilder.AlterColumn<long>(
                name: "ProductCategoryId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "دسته بندی محصول");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "حذف شدگی");

            migrationBuilder.AlterColumn<long>(
                name: "GoodsManagerThirdPartyId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "مدیر کالا");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CreatorId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "ایجاد کننده");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "تاریخ ایجاد");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "2, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GoodsManagerAssignmentHistory",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsManagerAssignmentHistory_GoodsManagerAssignments_GoodsManagerAssignmentId",
                schema: "engineer",
                table: "GoodsManagerAssignmentHistory",
                column: "GoodsManagerAssignmentId",
                principalSchema: "engineer",
                principalTable: "GoodsManagerAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
