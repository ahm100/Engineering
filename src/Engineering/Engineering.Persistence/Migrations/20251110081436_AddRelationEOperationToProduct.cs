using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationEOperationToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EngineeringStandardProducts_EmployerOperations_EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts");

            migrationBuilder.DropIndex(
                name: "IX_EngineeringStandardProducts_EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts");

            migrationBuilder.DropColumn(
                name: "EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts");

            migrationBuilder.AddColumn<long>(
                name: "ConsumptionStandardProductId",
                schema: "engineer",
                table: "EmployerOperationProducts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperationProducts_ConsumptionStandardProductId",
                schema: "engineer",
                table: "EmployerOperationProducts",
                column: "ConsumptionStandardProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerOperationProducts_EngineeringStandardProducts_ConsumptionStandardProductId",
                schema: "engineer",
                table: "EmployerOperationProducts",
                column: "ConsumptionStandardProductId",
                principalSchema: "engineer",
                principalTable: "EngineeringStandardProducts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerOperationProducts_EngineeringStandardProducts_ConsumptionStandardProductId",
                schema: "engineer",
                table: "EmployerOperationProducts");

            migrationBuilder.DropIndex(
                name: "IX_EmployerOperationProducts_ConsumptionStandardProductId",
                schema: "engineer",
                table: "EmployerOperationProducts");

            migrationBuilder.DropColumn(
                name: "ConsumptionStandardProductId",
                schema: "engineer",
                table: "EmployerOperationProducts");

            migrationBuilder.AddColumn<long>(
                name: "EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EngineeringStandardProducts_EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts",
                column: "EmployerOperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EngineeringStandardProducts_EmployerOperations_EmployerOperationId",
                schema: "engineer",
                table: "EngineeringStandardProducts",
                column: "EmployerOperationId",
                principalSchema: "engineer",
                principalTable: "EmployerOperations",
                principalColumn: "Id");
        }
    }
}
