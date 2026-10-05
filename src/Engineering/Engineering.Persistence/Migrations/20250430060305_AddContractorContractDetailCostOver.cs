using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContractorContractDetailCostOver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractorContractDetailCostOvers",
                schema: "engineer",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "2, 1"),
                    ContractorId = table.Column<long>(type: "bigint", nullable: false),
                    Percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    ContractorContractDetailId = table.Column<long>(type: "bigint", nullable: false),
                    CostOverId = table.Column<long>(type: "bigint", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdaterId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorContractDetailCostOvers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractorContractDetailCostOvers_ContractorContractDetails_ContractorContractDetailId",
                        column: x => x.ContractorContractDetailId,
                        principalSchema: "engineer",
                        principalTable: "ContractorContractDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractorContractDetailCostOvers_CostOvers_CostOverId",
                        column: x => x.CostOverId,
                        principalSchema: "engineer",
                        principalTable: "CostOvers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetailCostOvers_ContractorContractDetailId",
                schema: "engineer",
                table: "ContractorContractDetailCostOvers",
                column: "ContractorContractDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractorContractDetailCostOvers_CostOverId",
                schema: "engineer",
                table: "ContractorContractDetailCostOvers",
                column: "CostOverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractorContractDetailCostOvers",
                schema: "engineer");
        }
    }
}
