using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHasChangedToOI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasChanged",
                schema: "engineer",
                table: "OperationInfos",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "تغییر داده شده یا نه");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasChanged",
                schema: "engineer",
                table: "OperationInfos");
        }
    }
}
