using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCategoryIdFromProjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_EngineeringCategories_CategoryId",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CategoryId",
                schema: "engineer",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "engineer",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                schema: "engineer",
                table: "Projects",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CategoryId",
                schema: "engineer",
                table: "Projects",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_EngineeringCategories_CategoryId",
                schema: "engineer",
                table: "Projects",
                column: "CategoryId",
                principalSchema: "engineer",
                principalTable: "EngineeringCategories",
                principalColumn: "Id");
        }
    }
}
