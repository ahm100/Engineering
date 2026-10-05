using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractorStatusStatementCostOver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractorStatusStatementCostOvers",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    RequestRewardId = table.Column<long>(type: "bigint", nullable: false),
                    ContractorStatusStatementId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorStatusStatementCostOvers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorStatusStatementCostOvers_ContractorContractDetailCostOvers_RequestRewardId",
                        column: x => x.RequestRewardId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContractDetailCostOvers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ContractorStatusStatementCostOvers_ContractorStatusStatements_ContractorStatusStatementId",
                        column: x => x.ContractorStatusStatementId,
                        principalSchema: "engineer",
                        principalTable: "ContractorStatusStatements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatusStatementCostOvers_ContractorStatusStatementId",
                schema: "engineer",
                table: "ContractorStatusStatementCostOvers",
                column: "ContractorStatusStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorStatusStatementCostOvers_RequestRewardId",
                schema: "engineer",
                table: "ContractorStatusStatementCostOvers",
                column: "RequestRewardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorStatusStatementCostOvers",
                schema: "engineer");
        }
    }
}
