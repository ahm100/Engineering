using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBoolIsPriceList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPriceList",
                schema: "engineer",
                table: "OperationInfos",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "فهرست بها هست یا نه");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPriceList",
                schema: "engineer",
                table: "OperationInfos");
        }
    }
}
