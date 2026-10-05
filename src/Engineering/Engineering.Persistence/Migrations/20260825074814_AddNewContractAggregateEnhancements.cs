using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewContractAggregateEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                schema: "engineer",
                table: "Contracts");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "Contracts",
                type: "nvarchar(1500)",
                maxLength: 1500,
                nullable: true,
                comment: "توضیحات");

            migrationBuilder.AddColumn<string>(
                name: "EnTitle",
                schema: "engineer",
                table: "Contracts",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "عنوان انگلیسی");

            migrationBuilder.AddColumn<string>(
                name: "FaTitle",
                schema: "engineer",
                table: "Contracts",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                comment: "عنوان فارسی");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "engineer",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "EnTitle",
                schema: "engineer",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "FaTitle",
                schema: "engineer",
                table: "Contracts");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "engineer",
                table: "Contracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                comment: "عنوان");
        }
    }
}
