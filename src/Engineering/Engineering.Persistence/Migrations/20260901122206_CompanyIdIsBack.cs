using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CompanyIdIsBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                schema: "engineer",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "شناسه کمپانی");

            migrationBuilder.AddColumn<long>(
                name: "CompanyId",
                schema: "engineer",
                table: "ContractorContracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "شناسه کمپانی");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "engineer",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                schema: "engineer",
                table: "ContractorContracts");
        }
    }
}
