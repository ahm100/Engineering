using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fixedAdvancePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "EmployerDocs",
                type: "nvarchar(1500)",
                nullable: true,
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldComment: "توضیحات");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdvancePayment",
                schema: "engineer",
                table: "EmployerContracts",
                type: "decimal(5,2)",
                nullable: false,
                comment: "درصد پیش پرداخت",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "پیش پرداخت");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdvancePayment",
                schema: "engineer",
                table: "EmployerContractHistories",
                type: "decimal(18,2)",
                nullable: false,
                comment: "درصد پیش پرداخت",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "پیش پرداخت");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "engineer",
                table: "EmployerDocs",
                type: "nvarchar(1500)",
                nullable: false,
                defaultValue: "",
                comment: "توضیحات",
                oldClrType: typeof(string),
                oldType: "nvarchar(1500)",
                oldNullable: true,
                oldComment: "توضیحات");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdvancePayment",
                schema: "engineer",
                table: "EmployerContracts",
                type: "decimal(18,2)",
                nullable: false,
                comment: "پیش پرداخت",
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldComment: "درصد پیش پرداخت");

            migrationBuilder.AlterColumn<decimal>(
                name: "AdvancePayment",
                schema: "engineer",
                table: "EmployerContractHistories",
                type: "decimal(18,2)",
                nullable: false,
                comment: "پیش پرداخت",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "درصد پیش پرداخت");
        }
    }
}
