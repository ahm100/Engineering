using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangePPIsActiveDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "ProjectProducts",
                type: "bit",
                nullable: false,
                defaultValue: true,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "وضعیت فعال یا غیر فعال بودن");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                schema: "engineer",
                table: "ProjectProducts",
                type: "bit",
                nullable: false,
                comment: "وضعیت فعال یا غیر فعال بودن",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true,
                oldComment: "وضعیت فعال یا غیر فعال بودن");
        }
    }
}
