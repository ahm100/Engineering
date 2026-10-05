using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractAdjustmentMethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContractAdjustmentConfigurations_ContractId",
                schema: "engineer",
                table: "ContractAdjustmentConfigurations");

            migrationBuilder.AddColumn<int>(
                name: "AdjustmentMethod",
                schema: "engineer",
                table: "Contracts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractAdjustmentConfigurations_ContractId",
                schema: "engineer",
                table: "ContractAdjustmentConfigurations",
                column: "ContractId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContractAdjustmentConfigurations_ContractId",
                schema: "engineer",
                table: "ContractAdjustmentConfigurations");

            migrationBuilder.DropColumn(
                name: "AdjustmentMethod",
                schema: "engineer",
                table: "Contracts");

            migrationBuilder.CreateIndex(
                name: "IX_ContractAdjustmentConfigurations_ContractId",
                schema: "engineer",
                table: "ContractAdjustmentConfigurations",
                column: "ContractId",
                unique: true,
                filter: "[IsDeleted] = 0");
        }
    }
}
