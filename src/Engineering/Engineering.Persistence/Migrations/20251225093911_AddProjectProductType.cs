using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectProductType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectProductType",
                schema: "engineer",
                table: "ProjectProducts",
                type: "int",
                nullable: false,
                defaultValue: 1,
                comment: "نوع کالای پروژه");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectProductType",
                schema: "engineer",
                table: "ProjectProducts");
        }
    }
}
