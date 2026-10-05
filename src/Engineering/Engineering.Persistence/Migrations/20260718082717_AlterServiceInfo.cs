using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterServiceInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionEn",
                schema: "engineer",
                table: "EngineeringServices",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true,
                comment: "توضیحات انگلیسی");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionFa",
                schema: "engineer",
                table: "EngineeringServices",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true,
                comment: "توضیحات فارسی");

            migrationBuilder.AddColumn<string>(
                name: "ServiceInfoEnName",
                schema: "engineer",
                table: "EngineeringServices",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                comment: "عنوان انگلیس");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "engineer",
                table: "EngineeringServices",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEn",
                schema: "engineer",
                table: "EngineeringServices");

            migrationBuilder.DropColumn(
                name: "DescriptionFa",
                schema: "engineer",
                table: "EngineeringServices");

            migrationBuilder.DropColumn(
                name: "ServiceInfoEnName",
                schema: "engineer",
                table: "EngineeringServices");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "engineer",
                table: "EngineeringServices");
        }
    }
}
