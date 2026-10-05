using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCSSPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractorStatusStatementPayments",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ManagerConfirmedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrimaryManagerConfirmedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinalManagerConfirmedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentOrderId = table.Column<long>(type: "bigint", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TreasuryPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ContractorStatusStatementId = table.Column<long>(type: "bigint", nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorStatusStatementPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorStatusStatementPayments_ContractorStatusStatements_ContractorStatusStatementId",
                        column: x => x.ContractorStatusStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorStatusStatements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatusStatementPayments_ContractorStatusStatementId",
                schema: "engineer",
                table: "ContractorStatusStatementPayments",
                column: "ContractorStatusStatementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorStatusStatementPayments",
                schema: "engineer");
        }
    }
}
