using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseChangePriceToPOHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BasePrice",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "هزینه اولیه");

            migrationBuilder.AddColumn<decimal>(
                name: "ChangedPrice",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: " آخریم هزینه");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasePrice",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "ChangedPrice",
                schema: "engineer",
                table: "ProjectOperationHistories");
        }
    }
}
