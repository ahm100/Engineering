using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceInfoToEOService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "FinalValue",
                schema: "engineer",
                table: "ProjectOperationDetailConsumableVolumeMachineries",
                type: "decimal(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,5)");

            migrationBuilder.AddColumn<long>(
                name: "ServiceInfoId",
                schema: "engineer",
                table: "EmployerOperationServices",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperationServices_ServiceInfoId",
                schema: "engineer",
                table: "EmployerOperationServices",
                column: "ServiceInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerOperationServices_EngineeringServices_ServiceInfoId",
                schema: "engineer",
                table: "EmployerOperationServices",
                column: "ServiceInfoId",
                principalSchema: "engineer",
                principalTable: "EngineeringServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerOperationServices_EngineeringServices_ServiceInfoId",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.DropIndex(
                name: "IX_EmployerOperationServices_ServiceInfoId",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.DropColumn(
                name: "ServiceInfoId",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinalValue",
                schema: "engineer",
                table: "ProjectOperationDetailConsumableVolumeMachineries",
                type: "decimal(18,5)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)");
        }
    }
}
