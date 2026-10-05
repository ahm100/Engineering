using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNewParamsInCSS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FinalManagerConfirmed",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FinalManagerDescription",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "nvarchar(1500)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PrimaryManagerConfirmed",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryManagerDescription",
                schema: "engineer",
                table: "ContractorStatusStatements",
                type: "nvarchar(1500)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalManagerConfirmed",
                schema: "engineer",
                table: "ContractorStatusStatements");

            migrationBuilder.DropColumn(
                name: "FinalManagerDescription",
                schema: "engineer",
                table: "ContractorStatusStatements");

            migrationBuilder.DropColumn(
                name: "PrimaryManagerConfirmed",
                schema: "engineer",
                table: "ContractorStatusStatements");

            migrationBuilder.DropColumn(
                name: "PrimaryManagerDescription",
                schema: "engineer",
                table: "ContractorStatusStatements");
        }
    }
}
