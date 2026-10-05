using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddApprovedBudgetToPHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ApprovedBudget",
                schema: "engineer",
                table: "ProjectHistories",
                type: "decimal(18,2)",
                nullable: true,
                comment: "بودجه ی مصوب");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBudget",
                schema: "engineer",
                table: "ProjectHistories");
        }
    }
}
