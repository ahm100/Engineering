using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectThirdPartiesToEngConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ProjectThirdParties",
                schema: "engineer",
                table: "EngineeringConfigs",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "کلید تنظیمات دسترسی پروژه");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectThirdParties",
                schema: "engineer",
                table: "EngineeringConfigs");
        }
    }
}
