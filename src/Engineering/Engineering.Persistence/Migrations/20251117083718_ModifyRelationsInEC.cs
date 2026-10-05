using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />

    public partial class ModifyRelationsInEC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerContractHeads_Projects_ProjectId",
                schema: "engineer",
                table: "EmployerContractHeads");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                schema: "engineer",
                table: "EmployerContractHeads",
                newName: "CostCenterId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployerContractHeads_ProjectId",
                schema: "engineer",
                table: "EmployerContractHeads",
                newName: "IX_EmployerContractHeads_CostCenterId");

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                schema: "engineer",
                table: "EmployerContracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerContracts_ProjectId",
                schema: "engineer",
                table: "EmployerContracts",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerContractHeads_CostCenters_CostCenterId",
                schema: "engineer",
                table: "EmployerContractHeads",
                column: "CostCenterId",
                principalSchema: "engineer",
                principalTable: "CostCenters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerContracts_Projects_ProjectId",
                schema: "engineer",
                table: "EmployerContracts",
                column: "ProjectId",
                principalSchema: "engineer",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerContractHeads_CostCenters_CostCenterId",
                schema: "engineer",
                table: "EmployerContractHeads");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployerContracts_Projects_ProjectId",
                schema: "engineer",
                table: "EmployerContracts");

            migrationBuilder.DropIndex(
                name: "IX_EmployerContracts_ProjectId",
                schema: "engineer",
                table: "EmployerContracts");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "engineer",
                table: "EmployerContracts");

            migrationBuilder.RenameColumn(
                name: "CostCenterId",
                schema: "engineer",
                table: "EmployerContractHeads",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployerContractHeads_CostCenterId",
                schema: "engineer",
                table: "EmployerContractHeads",
                newName: "IX_EmployerContractHeads_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerContractHeads_Projects_ProjectId",
                schema: "engineer",
                table: "EmployerContractHeads",
                column: "ProjectId",
                principalSchema: "engineer",
                principalTable: "Projects",
                principalColumn: "Id");
        }
    }
}
