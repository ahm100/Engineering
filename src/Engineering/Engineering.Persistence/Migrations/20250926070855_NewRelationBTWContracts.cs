using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewRelationBTWContracts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                schema: "engineer",
                table: "ContractorContracts",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostCenterId",
                schema: "engineer",
                table: "ContractorContractHeaders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContracts_ProjectId",
                schema: "engineer",
                table: "ContractorContracts",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractHeaders_CostCenterId",
                schema: "engineer",
                table: "ContractorContractHeaders",
                column: "CostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorContractHeaders_CostCenters_CostCenterId",
                schema: "engineer",
                table: "ContractorContractHeaders",
                column: "CostCenterId",
                principalSchema: "engineer",
                principalTable: "CostCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorContracts_Projects_ProjectId",
                schema: "engineer",
                table: "ContractorContracts",
                column: "ProjectId",
                principalSchema: "engineer",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorContractHeaders_CostCenters_CostCenterId",
                schema: "engineer",
                table: "ContractorContractHeaders");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractorContracts_Projects_ProjectId",
                schema: "engineer",
                table: "ContractorContracts");

            migrationBuilder.DropIndex(
                name: "IX_ContractorContracts_ProjectId",
                schema: "engineer",
                table: "ContractorContracts");

            migrationBuilder.DropIndex(
                name: "IX_ContractorContractHeaders_CostCenterId",
                schema: "engineer",
                table: "ContractorContractHeaders");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "engineer",
                table: "ContractorContracts");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                schema: "engineer",
                table: "ContractorContractHeaders");
        }
    }
}
