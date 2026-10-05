using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixCostCenterTabelForType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCenterTypeId",
                schema: "engineer",
                table: "CostCenters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CostCenterTypeId",
                schema: "engineer",
                table: "CostCenters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
