using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewAmountParamsInCSS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PreferentialReferenceCode",
                schema: "engineer",
                table: "EngineeringServices",
                type: "uniqueidentifier",
                nullable: true,
                comment: "کد مرجع تفص?ل?",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "˜Ï ãÑÌÚ ÊÝÕ?á?");

            migrationBuilder.AlterColumn<decimal>(
                name: "RewardsAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductsAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PaymentedAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "PayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "FinesAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CostOversAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CanPayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FixedAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RemainingAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ServicedAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: false,
                comment: "مبلغ تایید پرداخت",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "مبلغ قابل پرداخت");

            //migrationBuilder.AddColumn<decimal>(
            //    name: "PayableAmount",
            //    schema: "engineer",
            //    table: "ContractorStatusStatementPayments",
            //    type: "decimal(18,2)",
            //    nullable: false,
            //    defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "ConfirmedBankAccountId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FixedContractPctAmount",
                schema: "engineer",
                table: "ContractorStatusStatementDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ManagerFixedContractPctAmount",
                schema: "engineer",
                table: "ContractorStatusStatementDetails",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectFixedContractPctAmount",
                schema: "engineer",
                table: "ContractorStatusStatementDetails",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FixedAmount",
                schema: "engineer",
                table: "ContractorStatusStatements");

            migrationBuilder.DropColumn(
                name: "RemainingAmount",
                schema: "engineer",
                table: "ContractorStatusStatements");

            migrationBuilder.DropColumn(
                name: "ServicedAmount",
                schema: "engineer",
                table: "ContractorStatusStatements");

            migrationBuilder.DropColumn(
                name: "PayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "ConfirmedBankAccountId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments");

            migrationBuilder.DropColumn(
                name: "FixedContractPctAmount",
                schema: "engineer",
                table: "ContractorStatusStatementDetails");

            migrationBuilder.DropColumn(
                name: "ManagerFixedContractPctAmount",
                schema: "engineer",
                table: "ContractorStatusStatementDetails");

            migrationBuilder.DropColumn(
                name: "ProjectFixedContractPctAmount",
                schema: "engineer",
                table: "ContractorStatusStatementDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "PreferentialReferenceCode",
                schema: "engineer",
                table: "EngineeringServices",
                type: "uniqueidentifier",
                nullable: true,
                comment: "˜Ï ãÑÌÚ ÊÝÕ?á?",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true,
                oldComment: "کد مرجع تفص?ل?");

            migrationBuilder.AlterColumn<decimal>(
                name: "RewardsAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductsAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PaymentedAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "FinesAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "DiscountPrice",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CostOversAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CanPayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PayableAmount",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                type: "decimal(18,2)",
                nullable: false,
                comment: "مبلغ قابل پرداخت",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "مبلغ تایید پرداخت");
        }
    }
}
