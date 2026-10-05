using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddIncreaseRateAndFixOIHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerOperationServices_OperationInfoServices_OperationInfoServiceId",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.AddColumn<decimal>(
                name: "IncreaseRate",
                schema: "engineer",
                table: "ProjectOperations",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 1m,
                comment: "ضریب افزایش");

            migrationBuilder.AlterColumn<string>(
                name: "OperationInfoName",
                schema: "engineer",
                table: "OperationInfoHistories",
                type: "nvarchar(max)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<long>(
                name: "OperationInfoServiceId",
                schema: "engineer",
                table: "EmployerOperationServices",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployerOperationServices_OperationInfoServices_OperationInfoServiceId",
                schema: "engineer",
                table: "EmployerOperationServices",
                column: "OperationInfoServiceId",
                principalSchema: "engineer",
                principalTable: "OperationInfoServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployerOperationServices_OperationInfoServices_OperationInfoServiceId",
                schema: "engineer",
                table: "EmployerOperationServices");

            migrationBuilder.DropColumn(
                name: "IncreaseRate",
                schema: "engineer",
                table: "ProjectOperations");

            migrationBuilder.AlterColumn<string>(
                name: "OperationInfoName",
                schema: "engineer",
                table: "OperationInfoHistories",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<long>(
                name: "OperationInfoServiceId",
                schema: "engineer",
                table: "EmployerOperationServices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
    }
}
