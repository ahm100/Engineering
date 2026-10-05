using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddManagerDescToRequestMachinery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerDescription",
                schema: "engineer",
                table: "RequestMachineryHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerDescription",
                schema: "engineer",
                table: "RequestMachineries",
                type: "nvarchar(1500)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerDescription",
                schema: "engineer",
                table: "RequestMachineryHistories");

            migrationBuilder.DropColumn(
                name: "ManagerDescription",
                schema: "engineer",
                table: "RequestMachineries");
        }
    }
}
