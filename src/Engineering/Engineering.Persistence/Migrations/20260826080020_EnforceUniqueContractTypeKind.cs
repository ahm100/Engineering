using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EnforceUniqueContractTypeKind : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContractTypes_ContractId",
                schema: "engineer",
                table: "ContractTypes");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypes_ContractId_Kind",
                schema: "engineer",
                table: "ContractTypes",
                columns: new[] { "ContractId", "Kind" },
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContractTypes_ContractId_Kind",
                schema: "engineer",
                table: "ContractTypes");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypes_ContractId",
                schema: "engineer",
                table: "ContractTypes",
                column: "ContractId");
        }
    }
}
