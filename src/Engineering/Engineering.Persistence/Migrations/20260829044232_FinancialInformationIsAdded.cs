using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FinancialInformationIsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdjustmentCapPercentage",
                schema: "engineer",
                table: "ContractTypes");

            migrationBuilder.CreateTable(
                name: "ContractFinancialInformations",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه")
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractId = table.Column<long>(type: "bigint", nullable: false, comment: "شناسه قرارداد"),
                    InitialAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "مبلغ اولیه قرارداد"),
                    CurrencyId = table.Column<long>(type: "bigint", nullable: false, comment: "ارز"),
                    HasPrepayment = table.Column<bool>(type: "bit", nullable: false, comment: "مشمول پیش‌پرداخت"),
                    ContractCeilingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "سقف مبلغ قرارداد"),
                    AdjustmentLimitValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "سقف افزایش/کاهش قرارداد"),
                    AdjustmentLimitType = table.Column<int>(type: "int", nullable: true, comment: "نوع سقف افزایش/کاهش قرارداد"),
                    PrepaymentPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true, comment: "درصد پیش‌پرداخت"),
                    PrepaymentAmortizationMethod = table.Column<int>(type: "int", nullable: true, comment: "روش استهلاک پیش‌پرداخت"),
                    PrepaymentAmortizationValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true, comment: "مقدار/درصد استهلاک پیش‌پرداخت"),
                    PrepaymentStartStatusStatementNumber = table.Column<int>(type: "int", nullable: true, comment: "شماره صورت‌وضعیت شروع استهلاک"),
                    PrepaymentStartProgressPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true, comment: "درصد پیشرفت شروع استهلاک"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "تاریخ ایجاد"),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false, comment: "ایجاد کننده"),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "تاریخ ویرایش"),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true, comment: "ویرایش کننده"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "حذف شدگی")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractFinancialInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractFinancialInformations_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "engineer",
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractFinancialInformations_ContractId",
                schema: "engineer",
                table: "ContractFinancialInformations",
                column: "ContractId",
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractFinancialInformations",
                schema: "engineer");

            migrationBuilder.AddColumn<decimal>(
                name: "AdjustmentCapPercentage",
                schema: "engineer",
                table: "ContractTypes",
                type: "decimal(18,5)",
                nullable: true,
                comment: "درصد سقف تعدیل");
        }
    }
}
