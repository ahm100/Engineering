using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTolerancePercentageToProjectProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TolerancePercentage",
                schema: "engineer",
                table: "ProjectProducts",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m,
                comment: "درصد تلورانس");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TolerancePercentage",
                schema: "engineer",
                table: "ProjectProducts");
        }
    }
}
