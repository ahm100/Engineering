using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectPrefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                schema: "engineer",
                table: "Projects",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "پیش شماره کدگذاری");

            migrationBuilder.AddColumn<string>(
                name: "Prefix",
                schema: "engineer",
                table: "ProjectHistories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "پیش شماره کدگذاری");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prefix",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Prefix",
                schema: "engineer",
                table: "ProjectHistories");
        }
    }
}
