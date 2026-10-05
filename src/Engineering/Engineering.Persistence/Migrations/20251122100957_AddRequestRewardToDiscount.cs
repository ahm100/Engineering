using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestRewardToDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                schema: "engineer",
                table: "OperationInfoHistories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                comment: "قیمت پایه",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<long>(
                name: "RequestRewardId",
                schema: "engineer",
                table: "ContractorStatusStatementDiscounts",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatusStatementDiscounts_RequestRewardId",
                schema: "engineer",
                table: "ContractorStatusStatementDiscounts",
                column: "RequestRewardId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatusStatementDiscounts_RequestRewards_RequestRewardId",
                schema: "engineer",
                table: "ContractorStatusStatementDiscounts",
                column: "RequestRewardId",
                principalSchema: "engineer",
                principalTable: "RequestRewards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorStatusStatementDiscounts_RequestRewards_RequestRewardId",
                schema: "engineer",
                table: "ContractorStatusStatementDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_ContractorStatusStatementDiscounts_RequestRewardId",
                schema: "engineer",
                table: "ContractorStatusStatementDiscounts");

            migrationBuilder.DropColumn(
                name: "RequestRewardId",
                schema: "engineer",
                table: "ContractorStatusStatementDiscounts");

            migrationBuilder.AlterColumn<decimal>(
                name: "BasePrice",
                schema: "engineer",
                table: "OperationInfoHistories",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 0m,
                oldComment: "قیمت پایه");
        }
    }
}
