using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangesForTestCostOverOnWF : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                schema: "engineer",
                table: "CostOvers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrentApprovalAttemptId",
                schema: "engineer",
                table: "CostOvers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                schema: "engineer",
                table: "CostOvers");

            migrationBuilder.DropColumn(
                name: "CurrentApprovalAttemptId",
                schema: "engineer",
                table: "CostOvers");
        }
    }
}
