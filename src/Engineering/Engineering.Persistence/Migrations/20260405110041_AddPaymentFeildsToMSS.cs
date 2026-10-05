using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Engineering.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentFeildsToMSS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CostCategoryId",
                schema: "engineer",
                table: "RequestMachineryStatusStatements",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CostGroupId",
                schema: "engineer",
                table: "RequestMachineryStatusStatements",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DocumentTypeId",
                schema: "engineer",
                table: "RequestMachineryStatusStatements",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "PreferentialTypeId",
                schema: "engineer",
                table: "RequestMachineryStatusStatements",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCategoryId",
                schema: "engineer",
                table: "RequestMachineryStatusStatements");

            migrationBuilder.DropColumn(
                name: "CostGroupId",
                schema: "engineer",
                table: "RequestMachineryStatusStatements");

            migrationBuilder.DropColumn(
                name: "DocumentTypeId",
                schema: "engineer",
                table: "RequestMachineryStatusStatements");

            migrationBuilder.DropColumn(
                name: "PreferentialTypeId",
                schema: "engineer",
                table: "RequestMachineryStatusStatements");
        }
    }
}
