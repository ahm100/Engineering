using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCostOverToCC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalCostOveredAmount",
                schema: "engineer",
                table: "ContractorContracts",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ContractorContractDetailId",
                schema: "engineer",
                table: "ContractorContractDetailCostOvers",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ContractorContractId",
                schema: "engineer",
                table: "ContractorContractDetailCostOvers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetailCostOvers_ContractorContractId",
                schema: "engineer",
                table: "ContractorContractDetailCostOvers",
                column: "ContractorContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorContractDetailCostOvers_ContractorContracts_ContractorContractId",
                schema: "engineer",
                table: "ContractorContractDetailCostOvers",
                column: "ContractorContractId",
                principalSchema: "engineer",
                principalTable: "ContractorContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorContractDetailCostOvers_ContractorContracts_ContractorContractId",
                schema: "engineer",
                table: "ContractorContractDetailCostOvers");

            migrationBuilder.DropIndex(
                name: "IX_ContractorContractDetailCostOvers_ContractorContractId",
                schema: "engineer",
                table: "ContractorContractDetailCostOvers");

            migrationBuilder.DropColumn(
                name: "TotalCostOveredAmount",
                schema: "engineer",
                table: "ContractorContracts");

            migrationBuilder.DropColumn(
                name: "ContractorContractId",
                schema: "engineer",
                table: "ContractorContractDetailCostOvers");

            migrationBuilder.AlterColumn<long>(
                name: "ContractorContractDetailId",
                schema: "engineer",
                table: "ContractorContractDetailCostOvers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);
        }
    }
}
