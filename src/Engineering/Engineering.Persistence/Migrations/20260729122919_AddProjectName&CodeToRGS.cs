using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectNameCodeToRGS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ReferenceId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypes",
                type: "bigint",
                nullable: true,
                comment: "شناسه محصول سفارش داده شده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه محصول سفارش داده شده");

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                schema: "engineer",
                table: "RequestGoodsSupplyTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "کد نوع پروژه");

            migrationBuilder.AddColumn<string>(
                name: "ProjectEnName",
                schema: "engineer",
                table: "RequestGoodsSupplyTypes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "نام پروژه");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                schema: "engineer",
                table: "RequestGoodsSupplyTypes",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "نام پروژه");

            migrationBuilder.AlterColumn<long>(
                name: "ReferenceId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeHistories",
                type: "bigint",
                nullable: true,
                comment: "شناسه محصول سفارش داده شده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه محصول سفارش داده شده");

            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeHistories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "کد نوع پروژه");

            migrationBuilder.AddColumn<string>(
                name: "ProjectEnName",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeHistories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "نام پروژه");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeHistories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "نام پروژه");

            migrationBuilder.AlterColumn<long>(
                name: "ReferenceId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetails",
                type: "bigint",
                nullable: true,
                comment: "شناسه محصول سفارش داده شده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه محصول سفارش داده شده");

            migrationBuilder.AlterColumn<long>(
                name: "ReferenceId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetailHistories",
                type: "bigint",
                nullable: true,
                comment: "شناسه محصول سفارش داده شده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "شناسه محصول سفارش داده شده");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertisementDocument",
                schema: "engineer");

            migrationBuilder.DropTable(
                name: "ServiceInfoDocument",
                schema: "engineer");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                schema: "engineer",
                table: "RequestGoodsSupplyTypes");

            migrationBuilder.DropColumn(
                name: "ProjectEnName",
                schema: "engineer",
                table: "RequestGoodsSupplyTypes");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                schema: "engineer",
                table: "RequestGoodsSupplyTypes");

            migrationBuilder.DropColumn(
                name: "ProjectCode",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeHistories");

            migrationBuilder.DropColumn(
                name: "ProjectEnName",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeHistories");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeHistories");

            migrationBuilder.AlterColumn<long>(
                name: "ReferenceId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "شناسه محصول سفارش داده شده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "شناسه محصول سفارش داده شده");

            migrationBuilder.AlterColumn<long>(
                name: "ReferenceId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "شناسه محصول سفارش داده شده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "شناسه محصول سفارش داده شده");

            migrationBuilder.AlterColumn<long>(
                name: "ReferenceId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "شناسه محصول سفارش داده شده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "شناسه محصول سفارش داده شده");

            migrationBuilder.AlterColumn<long>(
                name: "ReferenceId",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetailHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "شناسه محصول سفارش داده شده",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "شناسه محصول سفارش داده شده");
        }
    }
}
