using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHasProductToProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasProduct",
                schema: "engineer",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "کالا دارد یا خیر");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasProduct",
                schema: "engineer",
                table: "Projects");
        }
    }
}
