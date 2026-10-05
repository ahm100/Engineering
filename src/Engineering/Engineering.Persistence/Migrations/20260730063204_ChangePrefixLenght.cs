using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangePrefixLenght : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                schema: "engineer",
                table: "EngineeringCodingConfigs",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                comment: "پیشوند",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "پیشوند");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Prefix",
                schema: "engineer",
                table: "EngineeringCodingConfigs",
                type: "nvarchar(max)",
                nullable: false,
                comment: "پیشوند",
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldComment: "پیشوند");
        }
    }
}
