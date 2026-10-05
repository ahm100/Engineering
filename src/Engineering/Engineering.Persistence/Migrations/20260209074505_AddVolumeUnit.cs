using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddVolumeUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "VolumeRate",
                schema: "engineer",
                table: "FixAssetMachineryRates",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VolumeRate",
                schema: "engineer",
                table: "FixAssetMachineries",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolumeRate",
                schema: "engineer",
                table: "FixAssetMachineryRates");

            migrationBuilder.DropColumn(
                name: "VolumeRate",
                schema: "engineer",
                table: "FixAssetMachineries");
        }
    }
}
