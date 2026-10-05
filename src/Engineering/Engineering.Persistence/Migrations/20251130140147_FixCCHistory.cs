using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixCCHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCenterHistories_CostCenters_CostCenterTypesId",
                schema: "engineer",
                table: "CostCenterHistories");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenterHistories_CostCenterId",
                schema: "engineer",
                table: "CostCenterHistories",
                column: "CostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCenterHistories_CostCenters_CostCenterId",
                schema: "engineer",
                table: "CostCenterHistories",
                column: "CostCenterId",
                principalSchema: "engineer",
                principalTable: "CostCenters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCenterHistories_CostCenters_CostCenterId",
                schema: "engineer",
                table: "CostCenterHistories");

            migrationBuilder.DropIndex(
                name: "IX_CostCenterHistories_CostCenterId",
                schema: "engineer",
                table: "CostCenterHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCenterHistories_CostCenters_CostCenterTypesId",
                schema: "engineer",
                table: "CostCenterHistories",
                column: "CostCenterTypesId",
                principalSchema: "engineer",
                principalTable: "CostCenters",
                principalColumn: "Id");
        }
    }
}
