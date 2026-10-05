using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixedNameCostOver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorStatusStatementCostOvers_ContractorContractDetailCostOvers_RequestRewardId",
                schema: "engineer",
                table: "ContractorStatusStatementCostOvers");

            migrationBuilder.RenameColumn(
                name: "RequestRewardId",
                schema: "engineer",
                table: "ContractorStatusStatementCostOvers",
                newName: "ContractorContractDetailCostOverId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorStatusStatementCostOvers_RequestRewardId",
                schema: "engineer",
                table: "ContractorStatusStatementCostOvers",
                newName: "IX_ContractorStatusStatementCostOvers_ContractorContractDetailCostOverId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatusStatementCostOvers_ContractorContractDetailCostOvers_ContractorContractDetailCostOverId",
                schema: "engineer",
                table: "ContractorStatusStatementCostOvers",
                column: "ContractorContractDetailCostOverId",
                principalSchema: "engineer",
                principalTable: "ContractorContractDetailCostOvers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorStatusStatementCostOvers_ContractorContractDetailCostOvers_ContractorContractDetailCostOverId",
                schema: "engineer",
                table: "ContractorStatusStatementCostOvers");

            migrationBuilder.RenameColumn(
                name: "ContractorContractDetailCostOverId",
                schema: "engineer",
                table: "ContractorStatusStatementCostOvers",
                newName: "RequestRewardId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractorStatusStatementCostOvers_ContractorContractDetailCostOverId",
                schema: "engineer",
                table: "ContractorStatusStatementCostOvers",
                newName: "IX_ContractorStatusStatementCostOvers_RequestRewardId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatusStatementCostOvers_ContractorContractDetailCostOvers_RequestRewardId",
                schema: "engineer",
                table: "ContractorStatusStatementCostOvers",
                column: "RequestRewardId",
                principalSchema: "engineer",
                principalTable: "ContractorContractDetailCostOvers",
                principalColumn: "Id");
        }
    }
}
