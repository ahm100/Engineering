using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "engineer",
                table: "EmployerOperationHistories",
                newName: "UnitPrice");

            migrationBuilder.AddColumn<bool>(
                name: "GoodsInProgress",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "کالاهای در حال پیشرفت");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "int",
                nullable: true,
                comment: "اولویت");

            migrationBuilder.AddColumn<int>(
                name: "ProjectOperationStatus",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "int",
                nullable: false,
                defaultValue: 1,
                comment: "وضعیت شرح عملیات های پروژه");

            migrationBuilder.AddColumn<decimal>(
                name: "TolerancePercentage",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "decimal(18,5)",
                nullable: false,
                defaultValue: 0m,
                comment: "درصد تحمل");

            migrationBuilder.AddColumn<long>(
                name: "UnitOfMeasurementId",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "decimal(18,2)",
                nullable: true,
                comment: "هزینه واحد");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoodsInProgress",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "ProjectOperationStatus",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "TolerancePercentage",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurementId",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                schema: "engineer",
                table: "ProjectOperationHistories");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                schema: "engineer",
                table: "EmployerOperationHistories",
                newName: "Price");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "engineer",
                table: "ProjectOperationHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "هزینه");
        }
    }
}
