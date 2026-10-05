using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCodeTitleProjectWbs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                schema: "engineer",
                table: "ProjectWbses");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "engineer",
                table: "ProjectWbses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "engineer",
                table: "ProjectWbses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                comment: "کد");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "engineer",
                table: "ProjectWbses",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "عنوان");
        }
    }
}
