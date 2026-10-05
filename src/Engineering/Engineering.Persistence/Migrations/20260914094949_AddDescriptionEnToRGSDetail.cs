using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionEnToRGSDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetails",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ProjectManager",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                comment: "مدیر پروژه",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldComment: "مدیر پروژه");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "engineer",
                table: "RequestGoodsSupplyTypeDetails");

            migrationBuilder.AlterColumn<long>(
                name: "ProjectManager",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "مدیر پروژه",
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true,
                oldComment: "مدیر پروژه");
        }
    }
}
