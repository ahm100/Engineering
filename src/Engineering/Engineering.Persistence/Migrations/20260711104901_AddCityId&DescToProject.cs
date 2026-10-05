using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCityIdDescToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CityId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true,
                comment: "شناسه شهر");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true,
                comment: "توضیحات");

            migrationBuilder.AlterColumn<int>(
                name: "LagDays",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.AlterColumn<int>(
                name: "LagDays",
                schema: "engineer",
                table: "ProjectOperationDependencies",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
