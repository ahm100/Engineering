using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixVolumeAddRemainingVolume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "RemainingVolume",
                schema: "engineer",
                table: "ProjectOperationDetailContractorServices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Volume",
                schema: "engineer",
                table: "ProjectOperationDetailContractorExperts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingVolume",
                schema: "engineer",
                table: "ProjectOperationDetailContractorServices");

            migrationBuilder.AlterColumn<long>(
                name: "Volume",
                schema: "engineer",
                table: "ProjectOperationDetailContractorExperts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
