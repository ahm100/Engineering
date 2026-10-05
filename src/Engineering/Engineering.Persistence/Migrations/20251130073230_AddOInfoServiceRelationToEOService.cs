using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOInfoServiceRelationToEOService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OperationInfoServiceId",
                schema: "engineer",
                table: "EmployerOperationServices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerOperationServices_OperationInfoServiceId",
                schema: "engineer",
                table: "EmployerOperationServices",
                column: "OperationInfoServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerOperationServices_OperationInfoServices_OperationInfoServiceId",
                schema: "engineer",
                table: "EmployerOperationServices",
                column: "OperationInfoServiceId",
                principalSchema: "engineer",
                principalTable: "OperationInfoServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerOperationServices_OperationInfoServices_OperationInfoServiceId",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.DropIndex(
                name: "IX_EmployerOperationServices_OperationInfoServiceId",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.DropColumn(
                name: "OperationInfoServiceId",
                schema: "engineer",
                table: "EmployerOperationServices");
        }
    }
}
