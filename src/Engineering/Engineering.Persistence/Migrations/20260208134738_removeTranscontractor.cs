using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removeTranscontractor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransportationContractorId",
                schema: "engineer",
                table: "TransportationContractorPriceWeightHistories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TransportationContractorId",
                schema: "engineer",
                table: "TransportationContractorPriceWeightHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
