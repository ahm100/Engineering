using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ContractDurationUnitIsAddedToCTD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationUnit",
                schema: "engineer",
                table: "ContractTypeDetails",
                type: "int",
                nullable: true,
                comment: "واحد مدت زمان");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationUnit",
                schema: "engineer",
                table: "ContractTypeDetails");
        }
    }
}
