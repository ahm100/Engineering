using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewRelOfDocAndPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ContractorStatusStatementPaymentId",
                schema: "engineer",
                table: "ContractorStatusStatementDocuments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatusStatementDocuments_ContractorStatusStatementPaymentId",
                schema: "engineer",
                table: "ContractorStatusStatementDocuments",
                column: "ContractorStatusStatementPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractorStatusStatementDocuments_ContractorStatusStatementPayments_ContractorStatusStatementPaymentId",
                schema: "engineer",
                table: "ContractorStatusStatementDocuments",
                column: "ContractorStatusStatementPaymentId",
                principalSchema: "engineer",
                principalTable: "ContractorStatusStatementPayments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractorStatusStatementDocuments_ContractorStatusStatementPayments_ContractorStatusStatementPaymentId",
                schema: "engineer",
                table: "ContractorStatusStatementDocuments");

            migrationBuilder.DropIndex(
                name: "IX_ContractorStatusStatementDocuments_ContractorStatusStatementPaymentId",
                schema: "engineer",
                table: "ContractorStatusStatementDocuments");

            migrationBuilder.DropColumn(
                name: "ContractorStatusStatementPaymentId",
                schema: "engineer",
                table: "ContractorStatusStatementDocuments");
        }
    }
}
